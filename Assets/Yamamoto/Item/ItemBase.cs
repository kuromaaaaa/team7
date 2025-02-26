using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemBase : MonoBehaviour, PauseManager.IPauseable
{
    [SerializeField, Header("RespawnItem")] private UpdateItem updateItem;
    [SerializeField, Header("アイテム効果時間")] protected int _length;

    private CircleCollider2D _collider;
    private bool _isPaused;
    protected ViewFartGauge _viewFartGauge;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _viewFartGauge = FindAnyObjectByType<ViewFartGauge>().GetComponent<ViewFartGauge>();
    }

    protected virtual void Action(Collider2D other) { } // 効果が付与されたとき

    private async UniTask Destroy(Collider2D other)
    {
        var tokenSource = new CancellationTokenSource();
        float elapsed = 0f;
        float duration = _length;
        while (elapsed < duration)
        {
            if (!_isPaused) // ポーズ中でない場合のみ時間を進める
            {
                elapsed += Time.deltaTime;
            }
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: tokenSource.Token);
        }
        // await UniTask.Delay( _length * 1000, cancellationToken: tokenSource.Token);
        EndAction(other);
        tokenSource.Cancel();
        tokenSource.Dispose();
    }
    
    protected virtual void EndAction(Collider2D other) { } // 効果時間が終わったとき
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // todo: sound　アイテムゲット
        AudioManager.SE.SetVolume(0.05f);
        AudioManager.SE.Play(AudioClipType.SE_Player_ItemGet);
        Action(other);
        Destroy(other).Forget();
        updateItem.EnableItem();
        updateItem.RespawnItem().Forget();
        _collider.enabled = false;
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
