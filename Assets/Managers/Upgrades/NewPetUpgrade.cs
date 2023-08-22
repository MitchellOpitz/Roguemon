using UnityEngine;

[System.Serializable]
public class NewPetUpgrade : Upgrade
{
    public GameObject newPetPrefab; // Reference to the new pet prefab to add

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade(); // Call the base class method

        // Add the new pet to the player's party or perform other relevant actions
        // For example, you might use the PartyManager to add the new pet
        PartyManager.Instance.AddToParty(newPetPrefab);
    }
}
