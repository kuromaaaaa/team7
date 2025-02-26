using UnityEngine;

public class InitializePlayerPosition : MonoBehaviour
{
    private Vector3 _defaultPosition;
    private PlayerHP _playerHp;

    private void Start()
    {
        _defaultPosition = gameObject.transform.position;
        _playerHp = GetComponent<PlayerHP>();
        _playerHp.Initialize += InitializePosition;
    }

    private void InitializePosition()
    {
        Debug.Log(_defaultPosition);
        gameObject.transform.position = _defaultPosition;
    }
}