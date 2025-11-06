using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float radius, pushForce;
    [SerializeField] private LayerMask layer;

    private void OnEnable()
    {
        TriggerHitbox();
    }

    private void TriggerHitbox()
    {

        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, layer);
        foreach (Collider enemy in enemies)
        {
            enemy.gameObject.GetComponent<Rigidbody>().AddExplosionForce(pushForce, transform.position, radius);
        }
    }

    private void OnDrawGizmos()
    {
        // Check if any collider is inside the sphere
        bool detected = Physics.CheckSphere(transform.position, radius, layer);

        // Change color based on detection
        Gizmos.color = detected ? Color.green : Color.red;

        // Draw the sphere gizmo
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
