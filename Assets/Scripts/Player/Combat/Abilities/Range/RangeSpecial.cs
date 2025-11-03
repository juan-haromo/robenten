using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Player/Abilities/Range/Attack")]
public class RangeSpecial : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask worldLayer;

    public override void Activate(Player player)
    {
        //Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos.z = Camera.main.nearClipPlane;
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Camera.);
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
