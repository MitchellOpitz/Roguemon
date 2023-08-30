using UnityEngine;
using System.Collections;

public class Pet : MonoBehaviour
{
    public string petName;
    public PetType type1;
    public PetType type2;
    public PetClass petClass;
    public int baseMaxHealth;
    public int currentHealth;
    public int defense;
    public int attack;
    public float maxMana;
    public float currentMana;
    public float baseManaGainPerAttack;
    public GameObject bulletPrefab;
    public SpecialAbility specialAbility;
    public int electricBounces;
    public float fightingMultiplier;
    public float poisonDamage;


    private float attackCooldown = 2f;
    private float timeSinceLastAttack = 0f;
    private PartyManager partyManager;
    private float manaGainPerAttack;
    private int currentMaxHealth;
    private bool isInvulnerable;
    private float fireMultiplier;

    private void Start()
    {
        // Initialize current health and mana
        ResetMaxHealth();
        currentHealth = currentMaxHealth;

        ResetManaGain();
        currentMana = 0;

        isInvulnerable = false;

        // Get reference to PartyManager
        partyManager = FindAnyObjectByType<PartyManager>();
    }

    private void Update()
    {
        // Keep track of time since last attack
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackCooldown)
        {
            timeSinceLastAttack = 0f;
            Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        // Calculate actual damage considering defense
        float actualDamage = Mathf.Max(0, damage - defense);
        CheckPushback();
        if(type1 == PetType.Fighting || type2 == PetType.Fighting)
        {
            actualDamage = actualDamage - (actualDamage * fightingMultiplier);
        }
        currentHealth -= (int)actualDamage;
        Debug.Log(damage + " damage taken.  New health: " + currentHealth);
        SteelSynergyCheck();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        // Find closest enemy and attack
        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if ((type1 == PetType.Fire || type2 == PetType.Fire) && fireMultiplier > 0)
            {
                Debug.Log("Fire type detected.  Applying burn effect.");
                bulletComponent.AddBurnEffect(fireMultiplier);
            }
            if (bulletComponent != null)
            {
                bulletComponent.SetTarget(target.transform);
            }
            if ((type1 == PetType.Electric || type2 == PetType.Electric))
            {
                bulletComponent.electricBounces = electricBounces;
                bulletComponent.isElectric = true;
            }
            if ((type1 == PetType.Poison || type2 == PetType.Poison) && poisonDamage > 0)
            {
                Debug.Log("Poison type detected.  Applying Poison effect.");
                bulletComponent.AddPoisonEffect(poisonDamage);
            }

            GainMana(manaGainPerAttack);
        }
    }

    public void GainMana(float manaAmount)
    {
        // Increase current mana and cast special ability if mana reaches max
        currentMana += manaAmount;

        if (currentMana >= maxMana)
        {
            CastSpecial();
            currentMana = 0;
        }
    }

    public void CastSpecial()
    {
        // Cast the special ability using accumulated mana
        specialAbility.Activate(this);
        currentMana = 0;
    }

    public void Die()
    {
        // Handle pet's death
        partyManager.PetDied(gameObject);
        Destroy(gameObject);
    }

    private GameObject FindClosestEnemy()
    {
        // Find the closest enemy within the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            GameObject nearestEnemy = null;
            float closestDistance = Mathf.Infinity;

            // Iterate through enemies to find the nearest one
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }
        return null; // Return null if no enemies found
    }

    private IEnumerator PushbackEnemyOverTime(Enemy enemy, Vector3 initialPosition, Vector3 pushbackDirection, float pushbackDistance, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 newPosition = Vector3.Lerp(initialPosition, initialPosition + pushbackDirection * pushbackDistance, t);
            enemy.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void CheckPushback()
    {
        if (GetComponent<DetectNearbyEnemies>().CheckPushback())
        {
            Debug.Log("Pushing enemy back.");
            Enemy enemy = FindClosestEnemy().GetComponent<Enemy>();
            Vector3 enemyMoveDirection = -enemy.transform.forward; // Move in the opposite direction the enemy is facing
            float pushbackDistance = 5f;
            float pushbackDuration = 0.25f; // Duration of the pushback

            Vector3 enemyInitialPosition = enemy.transform.position;

            StartCoroutine(PushbackEnemyOverTime(enemy, enemyInitialPosition, enemyMoveDirection, pushbackDistance, pushbackDuration));
        }
    }


    public void ResetManaGain()
    {
        manaGainPerAttack = baseManaGainPerAttack;
        // Debug.Log("Updating mana per attack.  New value: " + manaGainPerAttack);
    }

    public void UpdateManaGain(float multiplier)
    {
        manaGainPerAttack = baseManaGainPerAttack * (1 + multiplier);
        // Debug.Log("Updating mana per attack.  New value: " + manaGainPerAttack);
    }

    public void ResetMaxHealth()
    {
        currentMaxHealth = baseMaxHealth;
        // Debug.Log("Updating max health.  New value: " + currentMaxHealth);
    }

    public void UpdateMaxHealth(float multiplier)
    {
        currentMaxHealth = (int)(baseMaxHealth * (1 + multiplier));
        // Debug.Log("Updating max health.  New value: " + currentMaxHealth);
    }

    public void ResetFireMultiplier()
    {
        fireMultiplier = 0;
        Debug.Log("Updating fire multiplier.  New value: " + fireMultiplier);
    }

    public void UpdateFireMultipler(float multiplier)
    {
        fireMultiplier = multiplier;
        Debug.Log("Updating fire multiplier.  New value: " + fireMultiplier);
    }

    public void ResetFightingMultiplier()
    {
        fightingMultiplier = 0;
        Debug.Log("Updating fighting multiplier.  New value: " + fightingMultiplier);
    }

    public void UpdateFightingMultipler(float multiplier)
    {
        fightingMultiplier = multiplier;
        Debug.Log("Updating fighting multiplier.  New value: " + fightingMultiplier);
    }

    public void ResetPoisonDamage()
    {
        poisonDamage = 0;
        Debug.Log("Updating poison damage.  New value: " + poisonDamage);
    }

    public void UpdatePoisonDamage(float multiplier)
    {
        poisonDamage = multiplier;
        Debug.Log("Updating poison damage.  New value: " + poisonDamage);
    }

    private void SteelSynergyCheck()
    {
        Debug.Log("Steel synergy found.");
        if (type1 == PetType.Steel || type2 == PetType.Steel)
        {
            // Check if health is at 50% or 10%
            Debug.Log("Steel synergy triggered.");
            float currentHealthPercentage = (float)currentHealth / (float)currentMaxHealth;
            if (currentHealthPercentage <= 0.5f || currentHealthPercentage <= 0.1f)
            {
                ApplyInvulnerability(2f); // Apply invulnerability for 2 seconds
            }
        }
    }

    private void ApplyInvulnerability(float duration)
    {
        Debug.Log("Applying invulnerability.");
        // Implement your logic to make the pet invulnerable for the specified duration.
        // This could involve disabling damage, playing an effect, etc.
        StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        // Implement the invulnerability effect here
        // For example, you might disable taking damage and play a visual effect
        isInvulnerable = true;

        yield return new WaitForSeconds(duration);

        // Remove the invulnerability effect
        isInvulnerable = false;
        Debug.Log("Removing invulnerability.");
    }

}
