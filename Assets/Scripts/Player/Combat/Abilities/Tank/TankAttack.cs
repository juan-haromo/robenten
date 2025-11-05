using UnityEngine;

[CreateAssetMenu(fileName = "TankAttak", menuName = "Player/Abilities/Tank/Attack")]
public class TankAttack : PlayerAbility
{
    public override void Activate(Player player)
    {
        player.animManager.PlayClip("Attack");
    }
}
