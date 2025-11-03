using System.Collections;
using UnityEngine;

public class MagnetProyectile : ProyectileClass
{
    [SerializeField] private float attractDuration, attractForce, attractInterval, attractRadius;
    [SerializeField] private LayerMask targetLayer;
    private Coroutine attractionCoroutine;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                //Hacerle daño
            }
            rb.linearVelocity = Vector3.zero;
            attractionCoroutine = StartCoroutine(AttractTargets());
            Invoke("StopAttraction", attractDuration);
        }
    }

    private IEnumerator AttractTargets()
    {
        while (true)
        {
            AttractTargetsInArea();
            yield return new WaitForSeconds(attractInterval);
        }
    }

    private void AttractTargetsInArea()
    {
        Collider[] targetsInArea = Physics.OverlapSphere(transform.position, attractRadius, targetLayer, QueryTriggerInteraction.Ignore);
    }

    private void StopAttraction()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
