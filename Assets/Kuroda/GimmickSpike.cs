using UnityEngine;

public class GimmickSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable dmg))
        {
            dmg.TakeDamage();
            AudioManager.SE.Play(AudioClipType.SE_Gimmick_Needle);
        }
    }
    
}
