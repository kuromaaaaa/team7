using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UpdateItem : MonoBehaviour
{
    [SerializeField, Header("スプライトがあるオブジェクト")] private GameObject _obj;
    [SerializeField, Header("消えてから再度出現するまでの時間")] private int _time;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public async UniTask RespawnItem()
    {
        var tokenSource = new CancellationTokenSource();
        await UniTask.Delay(_time * 1000, cancellationToken: tokenSource.Token);
        _collider.enabled = true;
        _obj.SetActive(true);
        tokenSource.Cancel();
        tokenSource.Dispose();
    }

    public void EnableItem()
    {
        _collider.enabled = false;
        _obj.SetActive(false);
    }
}
