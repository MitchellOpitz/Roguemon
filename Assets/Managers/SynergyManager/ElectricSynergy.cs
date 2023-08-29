using UnityEngine;

public class ElectricSynergy : Synergy
{
    public ElectricSynergy(int requiredCount)
    {
        synergyName = "Electric Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered electric synergy bonus.");
        int additionalBounces = GetAdditionalBounces(requiredCount);

        // Implement your logic for causing attacks to bounce to additional enemies
        // For example, you could modify the attack behavior of pets to target additional enemies when they attack
    }

    private int GetAdditionalBounces(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 4;
            default:
                return 0;
        }
    }
}
