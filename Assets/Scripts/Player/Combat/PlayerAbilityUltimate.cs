using UnityEngine;

public abstract class PlayerAbilityUltimate : PlayerAbility
{
    public float ultimateTime;

    public abstract void ExitUltimate(Player player);
}
