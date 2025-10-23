using UnityEngine;

public class AssasinDefensive : PlayerAbility
{
    public GameObject smokeBombPrefab;
    [SerializeField] float smokebombForce = 1;
    public override void Activate(Player player)
    {
       GameObject smokeBomb= Instantiate(smokeBombPrefab);
        smokeBomb.TryGetComponent<Rigidbody>(out Rigidbody rb);
        rb.AddForce((player.gameObject.transform.forward)*smokebombForce, ForceMode.Impulse);
    }
}
