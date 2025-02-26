using UnityEngine;

/// <summary> playerの大きさが変わるアイテム </summary>
public class Mushroom : ItemBase
{
    [SerializeField, Header("変更後の大きさ")] private Vector3 _scale;

    private Vector3 _originalScale;
    
    protected override void Action(Collider2D other)
    {
        var tr = other.GetComponent<Transform>();
        _originalScale = tr.localScale;
        tr.localScale = _scale;
        _viewFartGauge.GaugeColorChange(Color.cyan, _length);
    }

    protected override void EndAction(Collider2D other)
    {
        var tr = other.GetComponent<Transform>();
        tr.localScale = _originalScale;
    }
}
