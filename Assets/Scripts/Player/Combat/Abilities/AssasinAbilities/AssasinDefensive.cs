using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AssasinDefensive", menuName = "Player/Abilities/Assasin/AssasinDefensive")]
public class AssasinDefensive : PlayerAbility
{
    public GameObject smokeBombPrefab;
    [SerializeField] float smokebombForce = 1;
    public override void Activate(Player player)
    {
        player.playerAnimator.SetTrigger("Defensive");
        player.StartCoroutine(ThrowBomb(player.gameObject));
        
    }

    IEnumerator ThrowBomb(GameObject player) 
    {
       yield return new WaitForSeconds(.48f);
        GameObject smokeBomb = Instantiate(smokeBombPrefab,player.transform.localPosition+player.gameObject.transform.forward*1.5f+new Vector3(0,1,0),Quaternion.identity);
        smokeBomb.TryGetComponent<Rigidbody>(out Rigidbody rb);
        rb.AddForce((player.transform.forward) * smokebombForce, ForceMode.Impulse);
    }
}
