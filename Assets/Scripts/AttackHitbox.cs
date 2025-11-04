using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    LayerMask enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damagedEntity)&& !other.gameObject.CompareTag("Player"))
        {
            damagedEntity.Damage(10);
        }
    }
}
