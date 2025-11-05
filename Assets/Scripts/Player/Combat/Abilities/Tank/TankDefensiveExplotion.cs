using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDefensiveExplotion : MonoBehaviour
{
    private List<PlayerHealthSystem> enemies = new();
    public float damage = 0f;

    private void OnEnable()
    {
        StartCoroutine(TimeToExplode());
    }

    private void Explode()
    {
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(damage);
            //push enemy
            enemies.Remove(enemy);
        }

        damage = 0f;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem enemy))
        {
            enemies.Add(enemy);
        }
    }

    private IEnumerator TimeToExplode()
    {
        yield return new WaitForSeconds(1f);
        Explode();
    }
}
