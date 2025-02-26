using UnityEngine;

public class FarEffect : MonoBehaviour
{
    [SerializeField, Header("パーティクルシステム")] private GameObject _fart;
    [SerializeField, Header("おなら出る場所")] private Transform _muzzle;
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
        if (_fart)
        {
            GameObject fart = Instantiate(_fart);
            fart.transform.position = _muzzle.position;
            fart.transform.rotation = _muzzle.rotation;
        }
    }
}