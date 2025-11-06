using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Player/Abilities/Range/Attack")]
public class RangeAttack : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask worldLayer;
    private Coroutine rangeAttackCoroutine;

    public override void Activate(Player player)
    {
        //Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos.z = Camera.main.nearClipPlane;
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Camera.);
        if (player.canUseAbilities)
            rangeAttackCoroutine = player.StartCoroutine(OnRangeAttack(player));
    }

    public override void Deactivate(Player player)
    {
        if (rangeAttackCoroutine != null)
        {
            player.StopCoroutine(rangeAttackCoroutine);
            rangeAttackCoroutine = null;
        }
    }

    private IEnumerator OnRangeAttack(Player player)
    {
        while (true)
        {
            ShootArrow(player);
            yield return new WaitForSeconds(CooldownTime);
        }
    }

    private void ShootArrow(Player player)
    {
        Vector3 shootTarget;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, worldLayer))
            shootTarget = hit.point;
        else
            shootTarget = ray.origin + ray.direction * attackRange;
        Vector3 shootDirection = (shootTarget - player.proyectileSpawnpoint.position).normalized;
        GameObject newArrow = Instantiate(proyectilePrefab, player.proyectileSpawnpoint.position, Quaternion.LookRotation(shootDirection));
        if (newArrow.TryGetComponent(out ProyectileClass arrowScript))
            arrowScript.ShootInDirection(shootDirection);
    }
}
