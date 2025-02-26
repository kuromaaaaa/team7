using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 残機管理
/// </summary>
public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField, Header("最大残機")] private int _maxHp = 3;
    [SerializeField, Header("ダメージ量")] private int _damageValue = 1;
    [SerializeField] private string _animName = "Death";
    [SerializeField] private Animator _animator;
    private int _currentHp;
    private bool _canToGameOver;
    public event Action Initialize;
    public event Action<int> TakeDamageAction;

    private void Start()
    {
        _currentHp = _maxHp;
        _canToGameOver = true;
        if (!_animator) Debug.LogWarning("Animatorをアサインしてください");
    }

    public async void TakeDamage()
    {
        Debug.LogError("toge");
        FartGauge.Instance.FartStop = true;
        _currentHp -= _damageValue;
        // 死亡Anim➡初期位置に移動
        await ToInitializePosition();
        TakeDamageAction(_currentHp);
    }

    private void Update()
    {
        if (_currentHp <= 0 && _canToGameOver)
        {
            // 死亡Anim➡シーン遷移
            ToGameOver().Forget();
            _canToGameOver = false;
        }
    }

    private async UniTask ToGameOver()
    {
        if (_animator) _animator.Play(_animName);
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        await SceneLoader.Instance.FadeAndLoadSceneAsync("GameOver");
    }

    private async UniTask ToInitializePosition()
    {
        if (_animator) _animator.Play(_animName);
        await UniTask.Delay(2000);
        //await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"));
        Initialize?.Invoke();
        FartGauge.Instance.FartStop = false;
        _animator.Play("IDLE");
    }
}