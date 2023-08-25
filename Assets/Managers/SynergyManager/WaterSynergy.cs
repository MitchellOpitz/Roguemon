using UnityEngine;

public class WaterSynergy : Synergy
{
    public WaterSynergy(int requiredCount)
    {
        synergyName = "Water Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered water synergy bonus.");
        float manaProductionMultiplier = GetManaProductionMultiplier(requiredCount);

        // Implement your logic for increasing the team's mana production
        // For example, you could modify the mana production rate of pets to increase their mana production by manaProductionMultiplier
    }

    private float GetManaProductionMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 3:
                return 0.25f; // 25% increase
            case 5:
                return 0.5f; // 50% increase
            case 7:
                return 1f; // 100% increase
            default:
                return 0f; // No increase for other cases
        }
    }
}
