using UnityEngine;

[CreateAssetMenu(fileName = "AssasinAttack", menuName = "Player/Abilities/Assasin/AssasinAttack")]
public class AssasinAttack : PlayerAbility
{
    
    public override void Activate(Player player)
    {
        player.playerAnimator.SetTrigger("Special");
        player.playerAnimator.ResetTrigger("Special");
    }
}
