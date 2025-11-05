using UnityEngine;

public class TankDefensive : PlayerAbility
{
    public float activeTime = 10f;

    public override void Activate(Player player)
    {
        player.health.damageMultiplier = .5f;
        player.StartCoroutine(player.ActiveAbility(activeTime, this));
    }

    public override void Deactivate(Player player)
    {
        player.tankDefensiveExplotion.GetComponent<TankDefensiveExplotion>().damage = player.health.damageTaken;
        player.tankDefensiveExplotion.SetActive(true);

        player.health.absorbingDamage = false;
        player.health.damageTaken = 0;
        player.health.damageMultiplier = 1;
    }
}
