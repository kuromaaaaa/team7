using UnityEngine;

public class ChiliPepper : ItemBase
{
    [SerializeField, Header("変更後のスピード")] private float _speed;
    private float _originalValue;
    
    protected override void Action(Collider2D other)
    {
         // todo: 角度を変える速度が速くなる
         // todo: 戻り値あり（もともとのスピード -> originalValueに入れる）。引数に変更後のスピード
         _originalValue = other.GetComponent<Direction>().ChangeRotationSpeed(_speed);
         _viewFartGauge.GaugeColorChange(Color.red, _length);
    }

    protected override void EndAction(Collider2D other)
    {
        // todo: 角度を変える速度を元に戻す
        // todo: 戻り値あり（もともとのスピード -> 無視）。引数に変更後のスピード
        other.GetComponent<Direction>().ChangeRotationSpeed(_originalValue);
    }
}
