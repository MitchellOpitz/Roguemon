using UnityEngine;

public class WindSynergy : Synergy
{
    public WindSynergy(int requiredCount)
    {
        synergyName = "Wind Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        // Debug.Log("Triggered wind synergy bonus.");
        float speedMultiplier = GetSpeedMultiplier(requiredCount);

        // Implement your logic to increase the team's movement speed
        // You can access the movement speed property of your characters and modify it.

        PartyManager partyManager = GameObject.FindAnyObjectByType<PartyManager>();
        foreach (GameObject pet in partyManager.party)
        {
            pet.GetComponent<PlayerMovement>().UpdateSpeed(speedMultiplier);
            pet.GetComponent<Follow>().UpdateSpeed(speedMultiplier);
        }
    }

    private float GetSpeedMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.15f; // 15% speed increase
            case 3:
                return 0.3f; // 30% speed increase
            case 4:
                return 0.5f; // 50% speed increase
            default:
                return 0f; // No speed increase for other cases
        }
    }
}
