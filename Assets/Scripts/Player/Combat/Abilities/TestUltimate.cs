using UnityEngine;

[CreateAssetMenu(fileName = "TestUltimate", menuName = "Player/Abilities/TestUltimate")]
public class TestUltimate : PlayerAbilityUltimate
{
    public override void Activate(Player player)
    {
        Debug.Log("activate" + abilityName);
        player.StartCoroutine(player.UltimateTimeActive(ultimateTime));
    }

    public override void ExitUltimate(Player player)
    {
        Debug.Log("deactivate");
    }
}
