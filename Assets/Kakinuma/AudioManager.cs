using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public enum SoundType
{
    BGM,
    SE,
}

public enum AudioClipType
{
    BGM_Title,
    BGM_Stage,
    BGM_GameOver,
    BGM_Ending,
    SE_Click,
    SE_Gimmick_Needle,
    SE_Player_Jump,
    SE_Player_ItemGet,
    SE_Player_Die
}


public class AudioManager
{
    private static AudioManager _instance = new AudioManager();
    private static bool _isInitialized = false;
    
    public static AudioManager Instance => _instance;
    
    private const int AudioSourceCount = 2; // オーディオソースの数 SoundTypeCount
    private const float DefaultVolume = 1.0f; // 初期音量
    private const string AudioSourcesContainerName = "AudioSourcesContainer";
    private SoundPlayer[] _player = new SoundPlayer[AudioSourceCount];
    private BGMPlayer _bgmPlayer;
    private SEPlayer _sePlayer;
    private SoundDataAsset _soundDataAsset;

    AudioManager()
    {
        _player[(int)SoundType.BGM] = _bgmPlayer = new BGMPlayer();
        _player[(int)SoundType.SE] = _sePlayer = new SEPlayer();
    }

    public static BGMPlayer BGM => _instance._bgmPlayer;
    public static SEPlayer SE => _instance._sePlayer;

    /// <summary> プレイヤーの初期化 </summary>
    public static void Initialize()
    {
        if (_isInitialized) return;
        
        var obj = GameObject.Instantiate(SoundDataAsset.Entity.SoundPlayerParentObject);
        obj.name = AudioSourcesContainerName;
        GameObject.DontDestroyOnLoad(obj);
        _instance.InitializePlayer();
        _isInitialized = true;
    }

    private void InitializePlayer()
    {
        foreach (var player in _player)
        {
            player.Setup();
            player.SetVolume(DefaultVolume);
        }
    }

    public class SoundPlayer
    {
        private SoundType _type;
        protected float _volume = 1.0f;
        protected float _pitch = 1.0f;
        protected AudioSource _audioSource;

        /// <summary> プレイヤーの種類指定 </summary>
        /// <param name="type"> SoundType BGMやSE　</param>
        public SoundPlayer(SoundType type)
        {
            _type = type;
        }

        /// <summary> AudioSourceの生成 </summary>
        public virtual void Setup()
        {
            GameObject audioObject = new GameObject($"AudioSource_{_type}");
            audioObject.transform.parent = GameObject.Find(AudioSourcesContainerName).transform;
            //audioObject.hideFlags = HideFlags.HideInHierarchy; // AudioSourceを隠す
            _audioSource = audioObject.AddComponent<AudioSource>();
        }

        /// <summary> プレイヤー毎の音量設定 </summary>
        /// <param name="volume"> float 音量 </param>
        public virtual void SetVolume(float volume)
        {
            _volume = volume;
            _audioSource.volume = _volume;
        }

        /// <summary> プレイヤー毎のピッチ設定 </summary>
        /// <param name="pitch"> float ピッチ </param>
        public virtual void SetPitch(float pitch)
        {
            _pitch = pitch;
            _audioSource.pitch = _pitch;
        }

        /// <summary> 再生 </summary>
        /// <param name="type"> AudioClipTYpe 音の名前 </param>
        /// <param name="loopFlag"> bool ループするかどうか Default = false </param>
        public virtual void Play(AudioClipType type, bool loopFlag = false)
        {
            foreach (var data in SoundDataAsset.Entity.SoundSetList)
            {
                if (data.AudioClipType == type)
                {
                    _audioSource.PlayOneShot(data.AudioClip);
                    return;
                }
            }
            Debug.LogWarning($"{type}のサウンドデータが見つかりません。SoundDataAssetを確認してください");
        }

        /// <summary> 停止 </summary>
        public virtual void Stop()
        {
            _audioSource.Stop();
        }

        /// <summary> 一時停止 </summary>
        public virtual void Pause()
        {
            _audioSource.Pause();
        }

        /// <summary> 再開 </summary>
        public virtual void Resume()
        {
            _audioSource.UnPause();
        }
    }

    /// <summary> BGM用のプレイヤー </summary>
    public class BGMPlayer : SoundPlayer
    {
        public BGMPlayer() : base(SoundType.BGM){}
        
        /// <summary> 再生 </summary>
        /// <param name="type"> 音の種類 </param>
        /// <param name="loopFlag"> bool ループするかどうか Default = true </param>
        public override void Play(AudioClipType type, bool loopFlag = true)
        {
            _audioSource.loop = loopFlag;

            foreach (var data in SoundDataAsset.Entity.SoundSetList)
            {
                if (data.AudioClipType == type)
                {
                    _audioSource.clip = data.AudioClip;
                    _audioSource.Play();
                    return;
                }
            }
            Debug.LogWarning($"{type}のサウンドデータが見つかりません。SoundDataAssetを確認してください");
        }

        /// <summary> 音量のフェード </summary>
        /// <param name="vol"> 最終音量 </param>
        /// <param name="duration"> 指定の音量に鳴るまでにかかる時間 </param>
        /// <param name="stopFlag"> フェード後に音を止めるかどうか Default = false </param>
        /// <param name="easeType"> イージングの種類 Default = Ease.Unset </param>
        public async UniTask FadeVolume(float vol, float duration, bool stopFlag = false, Ease easeType = Ease.Unset)
        {
            await _audioSource.DOFade(vol, duration).SetEase(easeType).AsyncWaitForCompletion();
            if (stopFlag)
            {
                _audioSource.Stop();
            }
        }
    }
    
    /// <summary> SE用のプレイヤー </summary>
    public class SEPlayer : SoundPlayer
    {
        public SEPlayer() : base(SoundType.SE){}
    }
}
