using UnityEngine;

public class GimmickSpike : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable dmg))
        {
            dmg.TakeDamage();
        }
    }
}
