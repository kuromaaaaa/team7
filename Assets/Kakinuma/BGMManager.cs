using DG.Tweening;
using UnityEngine;

/// <summary> 全体のBGMを管理しておくクラス </summary>
public class BGMManager : SingletonMonoBehavior<BGMManager>
{
    [SerializeField, Header("次のBGM")] private AudioClipType _nextBGM = AudioClipType.BGM_Title;
    [SerializeField, Header("現在のBGM")] private AudioClipType _currentBGM;
    [SerializeField, Header("BGMをフェードする場合にかける時間")] private float _duration;

    [Header("BGM毎の音量")]
    [SerializeField, Header("スタート")] private float _startVolume = 0.03f;
    [SerializeField, Header("インゲーム")] private float _inGameVolume = 0.005f;
    [SerializeField, Header("ゲームオーバー")] private float _gameOverVolume = 0.03f;
    [SerializeField, Header("エンディング")] private float _endingVolume = 0.03f;
    
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
        switch (_currentBGM)
        {
            case AudioClipType.BGM_Title:
                AudioManager.BGM.SetVolume(_startVolume);
                break;
            case AudioClipType.BGM_Stage:
                AudioManager.BGM.SetVolume(_inGameVolume);
                break;
            case AudioClipType.BGM_GameOver:
                AudioManager.BGM.SetVolume(_gameOverVolume);
                break;
            case AudioClipType.BGM_Ending:
                AudioManager.BGM.SetVolume(_endingVolume);
                break;
        }

        AudioManager.BGM.Play(_currentBGM);
    }

    private void OnDisable()
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

        var loopFlag = true;

        // 音量設定
        switch (_nextBGM)
        {
            case AudioClipType.BGM_Title:
                AudioManager.BGM.SetVolume(_startVolume);
                break;
            case AudioClipType.BGM_Stage:
                AudioManager.BGM.SetVolume(_inGameVolume);
                break;
            case AudioClipType.BGM_GameOver:
                AudioManager.BGM.SetVolume(_gameOverVolume);
                loopFlag = false;
                break;
            case AudioClipType.BGM_Ending:
                AudioManager.BGM.SetVolume(_endingVolume);
                break;
        }
        
        AudioManager.BGM.Stop();
        AudioManager.BGM.Play(_nextBGM, loopFlag);
        _currentBGM = _nextBGM;
    }

    /// <summary> BGMの音量設定 </summary>
    /// <param name="vol"> float 音量 </param>
    public void SetVolume(float vol)
    {
        AudioManager.BGM.SetVolume(vol);
    }

    /// <summary> BGMの停止 </summary>
    public void StopBGM()
    {
        AudioManager.BGM.Stop();
    }
    
    /// <summary> BGMの一時停止 </summary>
    public void PauseBGM()
    {
        AudioManager.BGM.Pause();
    }
    
    /// <summary> BGMの再開/// </summary>
    public void ResumeBGM()
    {
        AudioManager.BGM.Resume();
    }
}
