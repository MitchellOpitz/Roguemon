using UnityEngine;

public class PsychicSynergy : Synergy
{
    public PsychicSynergy(int requiredCount)
    {
        synergyName = "Psychic Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered psychic synergy bonus.");
        float damageIncrease = GetDamageIncreasePercentage(requiredCount);

        // Implement your logic to increase damage dealt to nearby enemies
        // For example, you could increase the damage dealt by a certain percentage
        // to enemies within a certain radius of the pets with this synergy.
    }

    private float GetDamageIncreasePercentage(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.1f; // 10% increase
            case 3:
                return 0.3f; // 30% increase
            default:
                return 0f; // No increase for other cases
        }
    }
}
