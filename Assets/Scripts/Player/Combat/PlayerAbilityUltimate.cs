using UnityEngine;

public abstract class PlayerAbilityUltimate : PlayerAbility
{
    public string abilityName;
    public float ultimateTime;

    public abstract void ExitUltimate(Player player);
}
