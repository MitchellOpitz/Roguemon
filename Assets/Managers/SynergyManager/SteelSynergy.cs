using UnityEngine;

public class SteelSynergy : Synergy
{
    public SteelSynergy(int requiredCount)
    {
        synergyName = "Steel Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered steel synergy bonus.");

        // Implement your logic for granting invulnerability
        // For example, you could set a flag or apply an invincibility effect to pets when their health is at 50% or 10%
    }
}
