using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Player/Abilities/Range/Attack")]
public class RangeAttack : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float attackSpeed, attackRange;
    [SerializeField] private LayerMask worldLayer;
    private Coroutine rangeAttackCoroutine;

    public override void Activate(Player player)
    {
        //Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos.z = Camera.main.nearClipPlane;
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Camera.);
        if (player.canUseAbilities)
            rangeAttackCoroutine = player.StartCoroutine(OnRangeAttack());
    }

    public override void Deactivate(Player player)
    {
        if (rangeAttackCoroutine != null)
        {
            player.StopCoroutine(rangeAttackCoroutine);
            rangeAttackCoroutine = null;
        }
    }

    private IEnumerator OnRangeAttack()
    {
        while (rangeAttackCoroutine != null)
        {
            ShootArrow();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void ShootArrow()
    {
        Vector3 shootDirection;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, worldLayer))
        {
            //shootDirection = 
            Debug.Log("Hit object: " + hit.transform.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.green);
        }
    }
}
