using UnityEngine;

[System.Serializable]
public class UpgradeCurrentPet : Upgrade
{
    public StatToUpgrade statToUpgrade;
    public int amount;
    private Pet pet;

    public Upgrade InitializeUpgrade()
    {
        // Get the reference to the PartyManager
        PartyManager partyManager = PartyManager.Instance;

        // Get a random pet from the party list
        int randomPetIndex = Random.Range(0, partyManager.party.Count);
        pet = partyManager.party[randomPetIndex].GetComponent<Pet>();

        upgradeName = pet.name + " " + statToUpgrade.ToString() + " +" + amount;
        description = "Increase the " + statToUpgrade.ToString() + " of " + pet.name + " by " + amount + ".";

        return this;
    }

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade(); // Call the base class method

        // Add the new pet to the player's party or perform other relevant actions
        // For example, you might use the PartyManager to add the new pet

        switch (statToUpgrade)
        {
            case StatToUpgrade.Health:
                pet.maxHealth += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New health: " + pet.maxHealth);
                break;
            case StatToUpgrade.Attack:
                pet.attack += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New health: " + pet.attack);
                break;
            case StatToUpgrade.Defense:
                pet.defense += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New health: " + pet.defense);
                break;
        }
    }
}
