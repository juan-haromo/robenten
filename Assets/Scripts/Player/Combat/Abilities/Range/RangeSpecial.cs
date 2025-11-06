using UnityEngine;

[CreateAssetMenu(fileName = "RangeSpecial", menuName = "Player/Abilities/Range/Special")]
public class RangeSpecial : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask worldLayer;

    public override void Activate(Player player)
    {
        if(!PlayerCombat.Instance.IsSpecialCharged){ return; }
        PlayerCombat.Instance.UseSpecial();
        if (player.canUseAbilities)
            ShootSpecialArrow(player);
    }

    private void ShootSpecialArrow(Player player)
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
