using UnityEngine;

[CreateAssetMenu(fileName ="AssasinSpecial", menuName = "Player/Abilities/Assasin/AssasinSpecial")]
public class AssasinSpecial : PlayerAbility
{
    public int dashForce = 10;
    public override void Activate(Player player)
    {
        if(!PlayerCombat.Instance.IsSpecialCharged){ return; }
        PlayerCombat.Instance.UseSpecial();
        player.playerAnimator.SetTrigger("Special");
        
        player.movement.rb.AddRelativeForce(Vector3.forward*dashForce,ForceMode.Impulse);  
    }
}
