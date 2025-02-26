using UnityEngine;

public class Curry : ItemBase
{
    [SerializeField, Header("変更後の高さ")] private float _height;

    private float _originalHeight;
    
    protected override void Action(Collider2D other)
    {
        // todo: ジャンプの高さが高くなる
        // todo: 戻り値あり（もともとの高さ）。引数に変更後の高さ
        _originalHeight = other.GetComponent<Move>().ChangePower(_height);
        _viewFartGauge.GaugeColorChange(Color.yellow, _length);
    }

    protected override void EndAction(Collider2D other)
    {
        other.GetComponent<Move>().ChangePower(_originalHeight);
    }
}
