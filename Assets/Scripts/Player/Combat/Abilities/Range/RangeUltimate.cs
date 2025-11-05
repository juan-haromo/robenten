using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

[CreateAssetMenu(fileName = "RangeUltimate", menuName = "Player/Abilities/Range/Ultimate")]
public class RangeUltimate : PlayerAbility
{
    [SerializeField] private float takeoffForce, takeoffTime, flyTime;
    private Coroutine flyCoroutine;

    public override void Activate(Player player)
    {
        if (!player.canUseAbilities) return;
        player.canUseAbilities = false;
        FlyUp(player);
    }

    private void FlyUp(Player player)
    {
        if (player.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(Vector3.up * takeoffForce, ForceMode.Impulse);
            if (flyCoroutine == null)
                flyCoroutine = player.StartCoroutine(FlyFixedUpdate(rb));
        }
    }

    private IEnumerator FlyFixedUpdate(Rigidbody rb)
    {
        yield return new WaitForSeconds(takeoffTime);
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        float flyTimeCount = 0f;
        while (flyTimeCount < flyTime)
        {
            
            flyTimeCount += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        flyCoroutine = null;
        rb.useGravity = true;
        if (rb.gameObject.TryGetComponent(out Player player))
            player.canUseAbilities = true;
        yield break;
    }
}
