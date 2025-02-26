using UnityEngine;

/// <summary>
/// 真になる条件）通常：地面設置時／無重力：一定時間内継続
/// </summary>
public class Move : MonoBehaviour
{
    [SerializeField, Header("強さ")] private float _power = 5f;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Range(0, 1)] [SerializeField, Header("おならの消費量")]
    private float _fartConsumption = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private bool _canJump = true;
    private float _defaultPower;
    private bool _isZeroGravity;
    private JudgeGauge _judgeGauge;
    [SerializeField] private bool _isDebug;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _defaultPower = _power;
        _judgeGauge = GameObject.Find("JudgeGauge").gameObject.GetComponent<JudgeGauge>();
    }

    private void Update()
    {
        Jump();
    }


    private void Jump()
    {
        if (_isDebug)
        {
            if (Input.GetKeyDown(_jumpKey) && _canJump)
            {
                // AudioManager.SE.Play(AudioClipType.SE_Player_Jump);
                if (!_isZeroGravity) _canJump = false;
                Vector2 velocity = _rigidbody2D.linearVelocity;
                velocity += (Vector2)transform.up * _power;
                _rigidbody2D.linearVelocity = velocity;
            }
        }

        else if (Input.GetKeyDown(_jumpKey) && _canJump && _judgeGauge.Judge(_fartConsumption))
        {
            AudioManager.SE.Play(AudioClipType.SE_Player_Jump);
            if (!_isZeroGravity) _canJump = false;
            Vector2 velocity = _rigidbody2D.linearVelocity;
            velocity += (Vector2)transform.up * _power;
            _rigidbody2D.linearVelocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }

    public float ChangePower(float power)
    {
        _power = power;
        return _defaultPower;
    }

    public void ChangeFlag(bool flag)
    {
        _isZeroGravity = flag;
        _canJump = flag;
    }
}