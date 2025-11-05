using UnityEngine;

[CreateAssetMenu(fileName = "TankAttak", menuName = "Player/Abilities/Tank/Attack")]
public class TankAttack : PlayerAbility
{
    [SerializeField] private float radius, pushForce;

    public override void Activate(Player player)
    {
        player.combat.hitbox.radius = radius;
        player.combat.hitbox.pushForce = pushForce;

        player.animManager.PlayClip("Attack");
    }
}
