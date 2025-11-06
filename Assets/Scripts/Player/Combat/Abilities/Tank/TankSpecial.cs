using UnityEngine;

[CreateAssetMenu(fileName = "TankUltimate", menuName = "Player/Abilities/Tank/Special")]
public class TankSpecial : PlayerAbility
{
    [SerializeField] private float damage, pullForce;
    Vector3 hitpoint;

    public override void Activate(Player player)
    {
        if(!PlayerCombat.Instance.IsSpecialCharged){ return; }
        PlayerCombat.Instance.UseSpecial();
        //get shootpoint
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            hitpoint = hit.point;
        }

        if (Physics.Raycast(player.transform.position, hitpoint, out RaycastHit enemyPos))
        {
            if (enemyPos.transform.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(player.transform.position - rb.transform.position * pullForce, ForceMode.Impulse);
                if (enemyPos.transform.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.Damage(damage);
                }
            }
        }
    }
}
