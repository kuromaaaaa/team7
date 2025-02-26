using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField, Header("ボタンクリック時の音")] private AudioClipType _audioClipType = AudioClipType.SE_Click;

    public void OnClick()
    {
        AudioManager.SE.Play(_audioClipType);
    }
}