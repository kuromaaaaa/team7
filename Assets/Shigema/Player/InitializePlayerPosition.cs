using UnityEngine;

public class InitializePlayerPosition : MonoBehaviour
{
    private Transform _defaultPosition;
    private PlayerHP _playerHp;

    private void Start()
    {
        _defaultPosition = gameObject.transform;
        _playerHp = GetComponent<PlayerHP>();
        _playerHp.Initialize += InitializePosition;
    }

    private void InitializePosition()
    {
        gameObject.transform.position = _defaultPosition.position;
    }
}