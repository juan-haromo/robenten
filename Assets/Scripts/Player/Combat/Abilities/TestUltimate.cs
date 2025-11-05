using UnityEngine;

[CreateAssetMenu(fileName = "TestUltimate", menuName = "Player/Abilities/TestUltimate")]
public class TestUltimate : PlayerAbilityUltimate
{
    public float time;

    public override void Activate(Player player)
    {
        Debug.Log("activate" + abilityName);
        player.StartCoroutine(player.ActiveAbility(time, this));
    }

    public override void ExitUltimate(Player player)
    {
        Debug.Log("deactivate");
    }
}
