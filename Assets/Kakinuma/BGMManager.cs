using DG.Tweening;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField, Header("BGMの状態")] private AudioClipType _currentBGM = AudioClipType.BGM_Title;
    [SerializeField, Header("BGMをフェードする場合にかける時間")] private float _duration;

    public AudioClipType CurrentBGM
    {
        get => _currentBGM;
        set
        {
            _currentBGM = value;
            ChangeBGM(_currentBGM);
        }
    }

    private void Start()
    {
        // ToDo:BGMが決まったら
        //AudioManager.BGM.Play(_currentBGM, true);
    }


    public void FadeBGM(float vol, float duration, bool flag = false, Ease easeType = Ease.Unset)
    {
        AudioManager.BGM.FadeVolume(vol, duration, flag, easeType);
    }

    public void ChangeBGM(AudioClipType type)
    {
        AudioManager.BGM.Stop();
        AudioManager.BGM.Play(type, true);
    }
}
