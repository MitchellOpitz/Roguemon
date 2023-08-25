using UnityEngine;

public class FightingSynergy : Synergy
{
    public FightingSynergy(int requiredCount)
    {
        synergyName = "Fighting Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered fighting synergy bonus.");
        float damageMultiplier = GetDamageMultiplier(requiredCount);
        // Apply the damage multiplier to the contact damage of pets
        // For example, increase the pet's contact damage by damageMultiplier
    }

    private float GetDamageMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.1f; // 10% increase
            case 3:
                return 0.25f; // 25% increase
            case 4:
                return 0.5f; // 50% increase
            default:
                return 0f; // No increase for other cases
        }
    }
}
