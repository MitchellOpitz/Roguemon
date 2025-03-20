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

        PartyManager partyManager = GameObject.FindAnyObjectByType<PartyManager>();
        foreach (GameObject pet in partyManager.party)
        {
            Pet pet_ = pet.GetComponent<Pet>();
            if (pet_.type1 == PetType.Electric || pet_.type2 == PetType.Electric)
            {
                Debug.Log("Updating " + pet_.name + "'s electric bounces.");
                pet_.electricBounces = additionalBounces;
            }
        }
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
