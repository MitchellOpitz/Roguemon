using UnityEngine;

public class Pet : MonoBehaviour
{
    public string petName;
    public PetType type1;
    public PetType type2;
    public PetClass petClass;
    public int maxHealth;
    public int currentHealth;
    public int defense;
    public int attack;
    public float maxMana;
    public float currentMana;
    public float baseManaGainPerAttack;
    public GameObject bulletPrefab;

    private float attackCooldown = 2f;
    private float timeSinceLastAttack = 0f;
    private PartyManager partyManager;
    private float manaGainPerAttack;

    private void Start()
    {
        // Initialize current health and mana
        currentHealth = maxHealth;
        currentMana = 0;

        // Get reference to PartyManager
        partyManager = FindAnyObjectByType<PartyManager>();
        ResetManaGain();
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
        currentHealth -= actualDamage;

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
        // specialAbility.Activate(this);
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

    public void ResetManaGain()
    {
        manaGainPerAttack = baseManaGainPerAttack;
        Debug.Log("Updating mana per attack.  New value: " + manaGainPerAttack);
    }

    public void UpdateManaGain(float multiplier)
    {
        manaGainPerAttack = baseManaGainPerAttack * (1 + multiplier);
        Debug.Log("Updating mana per attack.  New value: " + manaGainPerAttack);
    }
}
