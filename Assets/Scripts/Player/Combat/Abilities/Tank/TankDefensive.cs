using UnityEngine;

public class TankDefensive : PlayerAbility
{
    public override void Activate(Player player)
    {
        player.health.damageMultiplier = .5f;
        player.StartCoroutine(player.ActiveAbility(CooldownTime, this));
    }

    public override void Deactivate(Player player)
    {
        player.health.damageMultiplier = 1;
    }
}
