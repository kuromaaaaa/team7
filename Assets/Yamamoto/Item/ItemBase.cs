using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("RespawnItem")] private UpdateItem updateItem;
    [SerializeField, Header("アイテム効果時間")] protected int _length;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Action(Collider2D other) { } // 効果が付与されたとき

    private async UniTask Destroy(Collider2D other)
    {
        var tokenSource = new CancellationTokenSource();
        await UniTask.Delay( _length * 1000, cancellationToken: tokenSource.Token);
        EndAction(other);
        tokenSource.Cancel();
        tokenSource.Dispose();
    }
    
    protected virtual void EndAction(Collider2D other) { } // 効果時間が終わったとき
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        // todo: sound　アイテムゲット
        // AudioManager.SE.Play(AudioClipType.SE_Player_ItemGet);
        Action(other);
        Destroy(other).Forget();
        updateItem.EnableItem();
        updateItem.RespawnItem().Forget();
        _collider.enabled = false;
    }
}
