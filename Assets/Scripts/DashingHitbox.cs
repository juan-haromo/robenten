using UnityEngine;

public class DashingHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damagedEntity)&&!other.gameObject.CompareTag("Player"))
        {
            damagedEntity.Damage(30);
        }
    }
}
