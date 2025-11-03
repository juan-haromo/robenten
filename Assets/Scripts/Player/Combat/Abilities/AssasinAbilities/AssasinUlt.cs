using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AssasinUlt", menuName = "Player/Abilities/Assasin/AssasinUlt")]
public class AssasinUlt : PlayerAbility
{
    List<GameObject> HitEnemies = new List<GameObject>();
    public bool shadowTime=false;
    public override void Activate(Player player)
    {
        player.playerAnimator.SetTrigger("Ult");
        player.StartCoroutine(ShadowForm(player.movement));
    }

    IEnumerator ShadowForm(PlayerMovement player) 
    {
        player.acceleration *= 2;
        player.maxMoveSpeed *= 2;
        shadowTime = true;
    yield return new WaitForSeconds(10);
        player.maxMoveSpeed /= 2;
        player.acceleration /= 2;
        shadowTime = false;
        foreach (GameObject item in HitEnemies) //reemplazae por hacer daño
        {
            Destroy(item);
        }
    }
}
