using UnityEngine;

public class FireSynergy : Synergy
{
    public FireSynergy(int requiredCount)
    {
        synergyName = "Fire Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered fire synergy bonus.");
        float damageMultiplier = GetDamageMultiplier(requiredCount);

        // Implement your logic to apply the burn effect to enemies on hit
        // You can access the damage calculations for your allies' attacks and add the burn damage over time effect.
    }

    private float GetDamageMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.33f; // 33% additional damage
            case 4:
                return 0.66f; // 66% additional damage
            case 6:
                return 1.0f; // 100% additional damage
            default:
                return 0f; // No additional damage for other cases
        }
    }
}
