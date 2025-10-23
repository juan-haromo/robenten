using UnityEngine;

[CreateAssetMenu(fileName = "TestAbility", menuName = "Player/Abilities/TestAbility")]
public class TestAbility : PlayerAbility
{
    [SerializeField] string abilityName;

    public override void Activate(Player player)
    {
        Debug.Log(player.name + " activated " + abilityName);
    }
}