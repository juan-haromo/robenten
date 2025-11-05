using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDefensiveExplotion : MonoBehaviour
{
    private List<PlayerHealthSystem> enemies = new();
    public float damage = 0f;


    public void Explode(float damage, float radius, float pushForce)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider enemy in enemies)
        {
            enemy.gameObject.GetComponent<IDamageable>().Damage(damage);
            enemy.gameObject.GetComponent<Rigidbody>().AddExplosionForce(pushForce, transform.position, radius);
        }

        damage = 0f;
        gameObject.SetActive(false);
    }
}
