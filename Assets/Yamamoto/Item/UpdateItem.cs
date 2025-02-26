using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UpdateItem : MonoBehaviour, PauseManager.IPauseable
{
    [SerializeField, Header("スプライトがあるオブジェクト")] private GameObject _obj;
    [SerializeField, Header("消えてから再度出現するまでの時間")] private int _time;

    private CircleCollider2D _collider;
    private bool _isPaused;
    private CancellationTokenSource _tokenSource;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _tokenSource = new CancellationTokenSource();
    }
    
    private void OnDestroy()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
    }

    public async UniTask RespawnItem()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
        _tokenSource = new CancellationTokenSource();
        // await UniTask.Delay(_time * 1000, cancellationToken: tokenSource.Token);
        float elapsed = 0f;
        float duration = _time;

        while (elapsed < duration)
        {
            if (!_isPaused) // ポーズ中でない場合のみ時間を進める
            {
                elapsed += Time.deltaTime;
            }
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _tokenSource.Token);
        }
        _collider.enabled = true;
        _obj.SetActive(true);
    }

    public void EnableItem()
    {
        _collider.enabled = false;
        _obj.SetActive(false);
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}
