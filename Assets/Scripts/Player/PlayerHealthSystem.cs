using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public float currentHealth, maxHealth = 100;

    public float damageMultiplier;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * damageMultiplier;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healing)
    {
        currentHealth += healing;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Me morí wey");
        gameObject.SetActive(false);
    }
}
