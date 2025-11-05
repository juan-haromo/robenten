using UnityEngine;

public class TankSpecial : PlayerAbility
{
    Vector3 hitpoint;

    public override void Activate(Player player)
    {
        //get shootpoint
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            hitpoint = hit.point;
        }

        if (Physics.Raycast(player.transform.position, hitpoint, out RaycastHit enemyPos))
        {
            if (enemyPos.transform.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem enemyHealth))
            {
                enemyHealth.TakeDamage(0);
            }
        }
    }
}
