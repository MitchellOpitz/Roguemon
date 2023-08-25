using UnityEngine;

public class FairySynergy : Synergy
{
    public FairySynergy(int requiredCount)
    {
        synergyName = "Fairy Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered fairy synergy bonus.");
        float fountainDuration = GetFountainDuration(requiredCount);

        // Implement your logic to periodically spawn healing fountains
        // For example, you could instantiate a healing fountain prefab at certain intervals
        // and destroy them after the fountainDuration has passed.
    }

    private float GetFountainDuration(int requiredCount)
    {
        switch (requiredCount)
        {
            case 1:
                return 3f; // 3 seconds
            case 2:
                return 5f; // 5 seconds
            case 3:
                return 10f; // 10 seconds
            default:
                return 0f; // No duration for other cases
        }
    }
}
