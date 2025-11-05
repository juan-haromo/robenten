using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "AssasinUlt", menuName = "Player/Abilities/Assasin/AssasinUlt")]
public class AssasinUlt : PlayerAbility
{
    List<IDamageable> HitEnemies = new List<IDamageable>();
    public bool shadowTime=false;
    LayerMask Enemies;
    public override void Activate(Player player)
    {
        
        player.playerAnimator.SetTrigger("Ult");
        player.StartCoroutine(ShadowForm(player.movement));
    }

    IEnumerator ShadowForm(PlayerMovement player) 
    {
        
        player.currentAcceleration *= 2;
        player.currentMaxMoveSpeed *= 2;
        shadowTime = true;
        player.StartCoroutine(ShadowAura(player.gameObject));
    yield return new WaitForSeconds(10);
        player.currentMaxMoveSpeed /= 2;
        player.currentAcceleration /= 2;
        shadowTime = false;
        foreach (IDamageable item in HitEnemies) //reemplazae por hacer daño
        {
            item.Damage(80);
        }
    }
    IEnumerator ShadowAura(GameObject player)
    {
        while (shadowTime)
        {
           Collider[] hitColliders =  Physics.OverlapSphere(player.transform.position, 1.5f, Enemies);
            foreach (var item in hitColliders)
            {
                if (item.gameObject.TryGetComponent<IDamageable>(out IDamageable enemy) && !item.gameObject.CompareTag("Player"))
                {
                    if (!HitEnemies.Contains(enemy))
                    {
                        HitEnemies.Add(enemy);
                        
                    }
                }
                if (item.gameObject.TryGetComponent<IStunnable>(out IStunnable stunEnemy) && !item.gameObject.CompareTag("Player"))
                {
                    stunEnemy.Stun();
                }
            }
            yield return null;
        }
        
    }
}
