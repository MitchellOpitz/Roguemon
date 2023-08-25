using UnityEngine;

public class GhostSynergy : Synergy
{
    public GhostSynergy(int requiredCount)
    {
        synergyName = "Ghost Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered ghost synergy bonus.");
        float teleportChance = GetTeleportChance(requiredCount);

        // Implement your logic to apply the teleportation effect
        // For example, you could roll a random number for each enemy
        // and if it's within the teleportChance range, teleport the enemy away.
    }

    private float GetTeleportChance(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.1f; // 10% chance
            case 4:
                return 0.25f; // 25% chance
            default:
                return 0f; // No chance for other cases
        }
    }
}
