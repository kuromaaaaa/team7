using DG.Tweening;
using UnityEngine;

/// <summary> 全体のBGMを管理しておくクラス </summary>
public class BGMManager : MonoBehaviour
{
    private static BGMManager _instance;
    public static BGMManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField, Header("次のBGM")] private AudioClipType _nextBGM = AudioClipType.BGM_Title;
    [SerializeField, Header("現在のBGM")] private AudioClipType _currentBGM;
    [SerializeField, Header("BGMをフェードする場合にかける時間")] private float _duration;
    
    /// <summary> 次に再生するBGM シーンロード時に反映される </summary>
    public AudioClipType NextBGM
    {
        get => _nextBGM;
        set => _nextBGM = value;
    }

    private void Start()
    {
        AudioManager.Initialize();
        SceneLoader.Instance.OnSceneLoaded += ChangeBGM;
        AudioManager.BGM.Play(_currentBGM);
    }

    private void OnDestroy()
    {
        SceneLoader.Instance.OnSceneLoaded -= ChangeBGM;
    }

    public void FadeBGM(float vol, float duration, bool flag = false, Ease easeType = Ease.Unset)
    {
        AudioManager.BGM.FadeVolume(vol, duration, flag, easeType);
    }

    /// <summary> BGMの変更 </summary>
    /// <param name="type"> AudioClipType 再生したいBGMの種類 </param>
    public void ChangeBGM()
    {
        // 同じBGMなら変更しない
        if (_currentBGM == _nextBGM) return;

        AudioManager.BGM.Stop();
        AudioManager.BGM.Play(_nextBGM);
        _currentBGM = _nextBGM;
    }

    /// <summary> BGMの音量設定 </summary>
    /// <param name="vol"> float 音量 </param>
    public void SetVolume(float vol)
    {
        AudioManager.BGM.SetVolume(vol);
    }
}
