using UnityEngine;

public class GroundSynergy : Synergy
{
    public GroundSynergy(int requiredCount)
    {
        synergyName = "Ground Synergy";
        this.requiredCount = requiredCount;
    }

    public override void ApplySynergyBonus()
    {
        Debug.Log("Triggered ground synergy bonus.");
        float healthMultiplier = GetHealthMultiplier(requiredCount);

        // Implement your logic for applying the bonus max health to the team
        // For example, you could modify the max health of pets to increase their health by healthMultiplier

        PartyManager partyManager = GameObject.FindAnyObjectByType<PartyManager>();
        foreach (GameObject pet in partyManager.party)
        {
            pet.GetComponent<Pet>().UpdateMaxHealth(healthMultiplier);
        }
    }

    private float GetHealthMultiplier(int requiredCount)
    {
        switch (requiredCount)
        {
            case 2:
                return 0.1f; // 10% increase
            case 4:
                return 0.3f; // 30% increase
            case 6:
                return 0.7f; // 70% increase
            case 8:
                return 1.3f; // 130% increase
            default:
                return 1f; // No increase for other cases
        }
    }
}
