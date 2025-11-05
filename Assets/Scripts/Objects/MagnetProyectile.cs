using System.Collections;
using UnityEngine;

public class MagnetProyectile : ProyectileClass
{
    [SerializeField] private float attractStartTime, attractDuration, attractForce, attractRadius, attractIncrease, radiusIncrease, increaseRate;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private ParticleSystem magneticEffect, outerEffect;
    private bool canAttract;

    protected override void Awake()
    {
        base.Awake();
        canAttract = false;
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
        //magneticEffect.transform.parent = null;
        //effectArea.transform.localScale = new Vector3(attractRadius * 2, attractRadius * 2, attractRadius * 2);
        //effectArea.transform.parent = transform;
        magneticEffect.transform.parent = null;
        magneticEffect.transform.localScale = new Vector3(attractRadius * 0.6f, attractRadius * 0.6f, attractRadius * 0.6f);
        magneticEffect.transform.parent = transform;
        outerEffect.transform.parent = null;
        outerEffect.transform.localScale = new Vector3(attractRadius * 0.6f, attractRadius * 0.6f, attractRadius * 0.6f);
        outerEffect.transform.parent = transform;
        //ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = effectSystem.sizeOverLifetime;
        //sizeOverLifetimeModule.size = new ParticleSystem.MinMaxCurve(0f, attractRadius * 2);
    }

    private void StartAttraction()
    {
        canAttract = true;
        //effectArea.transform.parent = null;
        //effectArea.transform.localScale = new Vector3(attractRadius * 2, attractRadius * 2, attractRadius * 2);
        //effectArea.transform.parent = transform;
        //effectArea.SetActive(false);
        magneticEffect.Play();
        outerEffect.Play();
        InvokeRepeating("IncreaseAttractionForceAndRadius", increaseRate, increaseRate);
        Invoke("StopAttraction", attractDuration);
    }

    private void StopAttraction()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
