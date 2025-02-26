using UnityEngine;

public class FarEffect : MonoBehaviour
{
    [SerializeField, Header("パーティクルシステム")] private ParticleSystem _fart;

    private void Start()
    {
        if (_fart == null)
        {
            Debug.LogWarning("おならのエフェクトがありません");
        }
    }

    /// <summary>
    /// ジャンプアニメーション中に呼ぶ
    /// </summary>
    public void FartEffect()
    {
        if (_fart) _fart.Play();
    }
}