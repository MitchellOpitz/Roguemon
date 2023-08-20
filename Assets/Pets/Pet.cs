using UnityEngine;

public class Pet : MonoBehaviour
{
    public string petName;
    public PetType type;
    public PetClass petClass;
    public int maxHealth;
    public int currentHealth;
    public int defense;
    public int attack;
    public int maxMana;
    public int currentMana;
    public int manaGainPerAttack;
    // public SpecialAbility specialAbility;

    private float attackCooldown = 2f;
    private float timeSinceLastAttack = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = 0;
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
        // Apply damage calculation, considering defense
        int actualDamage = Mathf.Max(0, damage - defense);
        currentHealth -= actualDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack() // param: Pet targetPet
    {
        // Attack logic, reduce target's health
        //targetPet.TakeDamage(attack);
        Debug.Log("Attacking for " + attack + " damage.");
        GainMana(manaGainPerAttack);
    }

    public void GainMana(int manaAmount)
    {
        currentMana += manaAmount;
        Debug.Log("Current mana: " + currentMana + " / " + maxMana);

        if (currentMana >= maxMana)
        {
            CastSpecial();
            currentMana = 0;
        }
    }

    public void CastSpecial()
    {
        //specialAbility.Activate(this);
        currentMana = 0;
        Debug.Log("Special cast.  Current mana: " + currentMana);
    }

    public void Die()
    {
        // Death logic, remove the pet from the game or handle it as needed
        Destroy(gameObject);
    }
}
