using UnityEngine;

[CreateAssetMenu(fileName = "TankUltimate", menuName = "Player/Abilities/Tank/Ultimate")]
public class TankUltimate : PlayerAbility
{
    public override void Activate(Player player)
    {
        player.TankUltimateBall.SetActive(true);

        CapsuleCollider collider = player.GetComponent<CapsuleCollider>();
        collider.radius = 1f;
        collider.height = 1f;

        player.movement.currentAcceleration = player.movement.normalAcceleration * 1.5f;
        player.movement.currentMaxMoveSpeed = player.movement.normalMaxMoveSpeed * 2f;

        player.StartCoroutine(player.ActiveAbility(CooldownTime, this));
    }

    public override void Deactivate(Player player)
    {
        player.movement.currentAcceleration = player.movement.normalAcceleration;
        player.movement.currentMaxMoveSpeed = player.movement.normalMaxMoveSpeed;

        CapsuleCollider collider = player.GetComponent<CapsuleCollider>();
        collider.radius = .5f;
        collider.height = 2f;

        player.TankUltimateBall.gameObject.SetActive(false);
    }
}
