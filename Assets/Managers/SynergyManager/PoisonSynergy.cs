using UnityEngine;

public class PoisonSynergy : Synergy
{
    public PoisonSynergy(int requiredCount)
    {
        synergyName = "Poison Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered poison synergy bonus.");
        float dotDamageMultiplier = GetDotDamageMultiplier(requiredCount);

        // Implement your logic to increase the damage over time (DoT) damage of allies
        // For example, you could modify the DoT damage applied to enemies to increase the damage by dotDamageMultiplier
    }

    private float GetDotDamageMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.2f; // 20% increase
            case 4:
                return 0.5f; // 50% increase
            default:
                return 0f; // No increase for other cases
        }
    }
}
