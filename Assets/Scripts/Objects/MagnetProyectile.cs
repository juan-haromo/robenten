using System.Collections;
using UnityEngine;

public class MagnetProyectile : ProyectileClass
{
    [SerializeField] private float attractStartTime, attractDuration, attractForce, attractRadius, attractIncrease, radiusIncrease, increaseRate;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject effectArea;
    private bool canAttract;

    protected override void Awake()
    {
        base.Awake();
        canAttract = false;
        effectArea.SetActive(false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                //Hacerle daño
            }
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            Invoke("StartAttraction", attractStartTime);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (canAttract)
        {
            AttractTargetsInArea();
        }
    }

    private void AttractTargetsInArea()
    {
        Collider[] targetsInArea = Physics.OverlapSphere(transform.position, attractRadius, targetLayer, QueryTriggerInteraction.Ignore);
        foreach (Collider target in targetsInArea)
        {
            if (target.gameObject.CompareTag(targetTag))
            {
                if (target.gameObject.TryGetComponent(out Rigidbody trb))
                {
                    Vector3 attractDirection = (transform.position - target.transform.position).normalized;
                    trb.AddForce(attractDirection * attractForce, ForceMode.Force);
                }
            }
        }
    }

    private void IncreaseAttractionForceAndRadius()
    {
        attractForce += attractIncrease;
        attractRadius += radiusIncrease;
        effectArea.transform.parent = null;
        effectArea.transform.localScale = new Vector3(attractRadius * 2, attractRadius * 2, attractRadius * 2);
        effectArea.transform.parent = transform;
    }

    private void StartAttraction()
    {
        canAttract = true;
        effectArea.transform.parent = null;
        effectArea.transform.localScale = new Vector3(attractRadius * 2, attractRadius * 2, attractRadius * 2);
        effectArea.transform.parent = transform;
        effectArea.SetActive(true);
        InvokeRepeating("IncreaseAttractionForceAndRadius", increaseRate, increaseRate);
        Invoke("StopAttraction", attractDuration);
    }

    private void StopAttraction()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
