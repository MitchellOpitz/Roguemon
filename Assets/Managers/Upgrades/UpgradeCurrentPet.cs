using UnityEngine;

[System.Serializable]
public class UpgradeCurrentPet : Upgrade
{
    public StatToUpgrade statToUpgrade;
    public int amount;
    private Pet pet;

    // Initializes the upgrade by selecting a random pet from the party and setting upgrade details.
    public Upgrade InitializeUpgrade()
    {
        // Get the reference to the PartyManager
        PartyManager partyManager = PartyManager.Instance;

        // Get a random pet from the party list
        int randomPetIndex = Random.Range(0, partyManager.party.Count);
        pet = partyManager.party[randomPetIndex].GetComponent<Pet>();

        // Set upgrade name and description based on the chosen pet and upgrade type
        upgradeName = pet.name.Replace("(Clone)","") + " " + statToUpgrade.ToString() + " +" + amount;
        description = "Increase the " + statToUpgrade.ToString() + " of " + pet.name.Replace("(Clone)", "") + " by " + amount + ".";

        return this;
    }

    // Applies the upgrade effects to the chosen pet's stats.
    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();

        // Apply the upgrade effects based on the chosen statToUpgrade value
        switch (statToUpgrade)
        {
            case StatToUpgrade.Health:
                pet.baseMaxHealth += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New health: " + pet.baseMaxHealth);
                break;
            case StatToUpgrade.Attack:
                pet.attack += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New attack: " + pet.attack);
                break;
            case StatToUpgrade.Defense:
                pet.defense += amount;
                Debug.Log("Pet " + pet.name + " upgraded.");
                Debug.Log("New defense: " + pet.defense);
                break;
        }
    }
}
