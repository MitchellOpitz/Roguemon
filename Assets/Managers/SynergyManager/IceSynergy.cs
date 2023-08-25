using UnityEngine;

public class IceSynergy : Synergy
{
    public IceSynergy(int requiredCount)
    {
        synergyName = "Ice Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered ice synergy bonus.");
        float slowAmount = GetSlowAmount(requiredCount);

        // Implement your logic to apply the slowing effect to nearby enemies
        // For example, you could iterate through enemies within a certain radius
        // and apply a speed reduction to them.
    }

    private float GetSlowAmount(int requiredCount)
    {
        switch (requiredCount)
        {
            case 3:
                return 0.2f; // 20% slow
            case 6:
                return 0.4f; // 40% slow
            default:
                return 0f; // No slow for other cases
        }
    }
}
