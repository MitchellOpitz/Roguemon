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


    private float attackCooldown = 2f;
    private float timeSinceLastAttack = 0f;
    private PartyManager partyManager;
    private Animator _animator;
    private float manaGainPerAttack;
    private int currentMaxHealth;
    private bool isInvulnerable;

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

        //Get the Animation controller on start
        _animator = GetComponentInChildren<Animator>();
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
        int actualDamage = Mathf.Max(0, damage - defense);
        CheckPushback();
        currentHealth -= actualDamage;
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
            _animator.SetTrigger("creatureAttack");

            if (bulletComponent != null)
            {
                bulletComponent.SetTarget(target.transform);
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
        // Handle pet's death animation
        _animator.SetTrigger("creatureDeath");
        // Trigger death cleanup after animation plays
        StartCoroutine(PetDeathCleanup());

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

    IEnumerator PetDeathCleanup() {
        yield return new WaitForSeconds(0.5f);
        partyManager.PetDied(gameObject);
        Destroy(gameObject);
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
