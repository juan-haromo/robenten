using UnityEngine;

[CreateAssetMenu(fileName = "TestAbility", menuName = "Player/Abilities/TestAbility")]
public class TestAbility : PlayerAbility
{
    public override void Activate(Player player)
    {
        Debug.Log(player.name + " activated " + abilityName);
    }
}