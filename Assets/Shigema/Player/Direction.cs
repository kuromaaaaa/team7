using System;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f; // 回転速度
    private float _defaultSpeed;

    private void Start()
    {
        _defaultSpeed = _rotationSpeed;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float rotationInput = 0f;

        rotationInput = -Input.GetAxis("Horizontal");

        // ローカルのZ軸回転を変更
        transform.Rotate(0f, 0f, rotationInput * _rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 回転速度を変更する
    /// </summary>
    /// <param name="speed"></param>
    /// <returns>デフォルトの回転速度</returns>
    public float ChangeRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
        return _defaultSpeed;
    }
}