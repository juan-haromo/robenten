using UnityEngine;

public class ProyectileClass : MonoBehaviour
{
    [SerializeField] protected float damage, speed;
    [SerializeField] protected string targetTag;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        TryGetComponent(out rb);
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
                //Hacerle daño
            }
            Destroy(gameObject);
        }
    }
}
