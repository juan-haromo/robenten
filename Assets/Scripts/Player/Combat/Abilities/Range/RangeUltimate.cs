using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

[CreateAssetMenu(fileName = "RangeUltimate", menuName = "Player/Abilities/Range/Ultimate")]
public class RangeUltimate : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float takeoffForce, takeoffTime, flyTime, attackRange;
    [SerializeField] private LayerMask worldLayer;
    private Coroutine flyCoroutine, shootCoroutine;

    public override void Activate(Player player)
    {
        if (!player.canUseAbilities) return;
        if(!PlayerCombat.Instance.IsUltimateCharged){ return; }
        PlayerCombat.Instance.UseUltimate();
        player.canUseAbilities = false;
        FlyUp(player);
    }

    private void FlyUp(Player player)
    {
        if (player.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(Vector3.up * takeoffForce, ForceMode.Impulse);
            if (flyCoroutine == null)
                flyCoroutine = player.StartCoroutine(FlyFixedUpdate(rb, player));
        }
    }

    private IEnumerator ShootFast(Player player)
    {
        while (true)
        {
            Shoot(player);
            yield return new WaitForSeconds(CooldownTime);
        }
    }

    private void Shoot(Player player)
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

    private IEnumerator FlyFixedUpdate(Rigidbody rb, Player player)
    {
        yield return new WaitForSeconds(takeoffTime);
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        if (shootCoroutine == null)
            shootCoroutine = player.StartCoroutine(ShootFast(player));
        yield return new WaitForSeconds(flyTime);
        flyCoroutine = null;
        player.StopCoroutine(shootCoroutine);
        shootCoroutine = null;
        rb.useGravity = true;
        player.canUseAbilities = true;
        yield break;
    }
}
