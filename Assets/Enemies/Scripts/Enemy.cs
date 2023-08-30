using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 100f;     // Current health of the enemy
    public float baseMoveSpeed = 3f;    // Speed at which the enemy moves
    public bool isSlowed;
    private float damageMultipler;

    public float currentMoveSpeed;

    private void Start()
    {
        ResetMoveSpeed();
    }

    // Called to apply damage to the enemy
    public virtual void TakeDamage(float damage)
    {
        health -= damage; // Reduce health by the damage amount
        if (health <= 0f)
        {
            Die(); // If health drops to or below 0, the enemy dies
        }
    }

    // Called when the enemy dies
    protected virtual void Die()
    {
        // Common death behavior for all enemy types
        EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
        spawner.EnemyDefeated(); // Notify the enemy spawner about the defeat
        Destroy(gameObject); // Destroy the enemy game object
    }

    public void ResetMoveSpeed()
    {
        currentMoveSpeed = baseMoveSpeed;
        isSlowed = false;
        // Debug.Log("Updating enemy move speed.  New value: " + currentMoveSpeed);
    }

    public void UpdateMoveSpeed(float multiplier)
    {
        currentMoveSpeed = baseMoveSpeed - (baseMoveSpeed * multiplier);
        isSlowed = true;
        // Debug.Log("Updating enemy move speed.  New value: " + currentMoveSpeed);
    }

    public void ResetDamageMultiplier()
    {
        damageMultipler = 1;
        // Debug.Log("Updating enemy damage multiplier.  New value: " + damageMultipler);
    }

    public void UpdateDamageMultiplier(float multiplier)
    {
        damageMultipler = 1 + multiplier;
        // Debug.Log("Updating enemy damage multiplier.  New value: " + damageMultipler);
    }
    public void ApplyBurnEffect(float damage, float damageMultiplier)
    {
        // Debug.Log("Burn effect activated.");
        float burnDuration = 3f; // Duration of burn effect
        float burnDamage = damage * damageMultiplier; // Calculate burn damage

        // Apply the burn effect to the enemy
        ApplyBurn(burnDamage, burnDuration);
    }

    public void ApplyBurn(float burnDamage, float duration)
    {
        // Debug.Log("Burn effect started.");
        StartCoroutine(BurnOverTime(burnDamage, duration));
    }

    private IEnumerator BurnOverTime(float burnDamage, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float damageThisFrame = burnDamage * Time.deltaTime;
            // Debug.Log("Fire damage taken: " + damageThisFrame);
            TakeDamage(damageThisFrame);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public void ApplyPoisonEffect(float damage, float damageMultiplier)
    {
        // Debug.Log("Poison effect activated.");
        float poisonDuration = 3f; // Duration of burn effect
        float poisonDamage = damage * damageMultiplier; // Calculate burn damage

        // Apply the burn effect to the enemy
        ApplyPoison(poisonDamage, poisonDuration);
    }

    public void ApplyPoison(float poisonDamage, float duration)
    {
        // Debug.Log("Burn effect started.");
        StartCoroutine(PoisonOverTime(poisonDamage, duration));
    }

    private IEnumerator PoisonOverTime(float poisonDamage, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float damageThisFrame = poisonDamage * Time.deltaTime;
            // Debug.Log("Poison damage taken: " + damageThisFrame);
            TakeDamage(damageThisFrame);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
