using UnityEngine;

public class ProyectileClass : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected string targetTag;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        TryGetComponent(out rb);
        Destroy(gameObject, 5);
    }

    public virtual void ShootInDirection(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                if(other.gameObject.TryGetComponent(out IDamageable dmg))
                {
                    dmg.Damage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
