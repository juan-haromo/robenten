using UnityEngine;

public class TankUltimateCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddExplosionForce(10, transform.position, 5);
            if (rb.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage(2);
            }
        }
    }
}
