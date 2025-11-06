using UnityEngine;
using System.Collections;
using UnityEditor;

[CreateAssetMenu(fileName = "RangeDefense", menuName = "Player/Abilities/Range/Defense")]
public class RangeDefense : PlayerAbility
{
    [SerializeField] private float takeoffForce, fallForce, pushForce, upPushForce, flyTime, pushRadius;
    [SerializeField] private string enemyTag;
    [SerializeField] private int pushDamage;
    [SerializeField] private LayerMask groundLayer, enemyLayer;
    private Coroutine flyCoroutine;

    public override void Activate(Player player)
    {
        if (!player.canUseAbilities) return;
        player.canUseAbilities = false;
        FlyUp(player);
    }

    private void FlyUp(Player player)
    {
        if(player.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(Vector3.up * takeoffForce, ForceMode.Impulse);
            PushEnemiesInArea(player.transform.position);
            if (flyCoroutine == null)
                flyCoroutine = player.StartCoroutine(FlyFixedUpdate(rb));
        }
    }

    private void PushEnemiesInArea(Vector3 playerPos)
    {
        Collider[] targetsInArea = Physics.OverlapSphere(playerPos, pushRadius, enemyLayer, QueryTriggerInteraction.Ignore);
        foreach (Collider target in targetsInArea)
        {
            if (target.gameObject.CompareTag(enemyTag))
            {
                if (target.gameObject.TryGetComponent(out IDamageable dmg))
                {
                    dmg.Damage(pushDamage);
                }
                //Aqui tambien se podria hacer da�o al enemigo con pushDamage
                if (target.gameObject.TryGetComponent(out Rigidbody trb))
                {
                    Vector3 pushDirection = (target.transform.position - playerPos).normalized;
                    trb.AddForce(Vector3.up * upPushForce, ForceMode.Impulse);
                    trb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
        }
    }

    private IEnumerator FlyFixedUpdate(Rigidbody rb)
    {
        yield return new WaitForSeconds(flyTime);
        //Debug.Log("Empieza a planear");
        rb.useGravity = false;
        //rb.linearVelocity = Vector3.zero;
        while (!IsOnGround(rb.gameObject.transform))
        {
            Debug.Log("eSRTOY ESPERANDO PISO");
            rb.AddForce(Vector3.down * fallForce, ForceMode.Force);
            yield return new WaitForFixedUpdate();
        }
        //LLego al suelo
        flyCoroutine = null;
        rb.useGravity = true;
        if (rb.gameObject.TryGetComponent(out Player player))
            player.canUseAbilities = true;
        yield break;
    }

    private bool IsOnGround(Transform player)
    {
        //Vector3 playerFeetPos = new Vector3(player.position.x, (player.position.y - player.lossyScale.y/2), player.position.z);
        Debug.DrawRay(player.position, Vector3.down * 0.4f, Color.red, 5f);
        if (Physics.Raycast(player.position, Vector3.down, 0.4f, groundLayer, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        return false;
    }
}
