using UnityEngine;

public class GetCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var starCoin = other.gameObject.GetComponent<StarCoin>();
        if (starCoin)
        {
            starCoin.GetCoin();
        }
    }
}