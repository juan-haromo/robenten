using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Player/Abilities/Range/Attack")]
public class RangeAttack : PlayerAbility
{
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float attackSpeed;
    private Coroutine rangeAttackCoroutine;

    public override void Activate(Player player)
    {
        //Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos.z = Camera.main.nearClipPlane;
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Camera.);
        rangeAttackCoroutine = player.StartCoroutine(OnRangeAttack());
    }

    public void Release(Player player)
    {
        player.StopCoroutine(rangeAttackCoroutine);
    }

    private IEnumerator OnRangeAttack()
    {
        //aca va a disparar xdxdxdxdxdxdDDDDDDD
        yield return null;
    }
}
