using UnityEngine;

public class Potato : ItemBase
{
    [SerializeField, Header("線形減衰量")] private float _value;
    
    private float _originalGravityScale; // GravityScaleの初期値
    private float _originalLinearDamping; // LinearDampingの初期値
    
    protected override void Action(Collider2D other)
    {
        // todo: 無重力にする
        other.GetComponent<Move>().ChangeFlag(true);
        var rb = other.GetComponent<Rigidbody2D>();
        _originalGravityScale = rb.gravityScale;
        _originalLinearDamping = rb.linearDamping;
        rb.gravityScale = 0; // 重力をなくす
        rb.linearDamping = _value;
    }
    
    protected override void EndAction(Collider2D other)
    {
        other.GetComponent<Move>().ChangeFlag(false);
        var rb = other.GetComponent<Rigidbody2D>();
        rb.gravityScale = _originalGravityScale;
        rb.linearDamping = _originalLinearDamping;
    }
}
