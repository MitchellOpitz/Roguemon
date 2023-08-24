using UnityEngine;

[System.Serializable]
public class NewPetUpgrade : Upgrade
{
    public GameObject newPetPrefab;

    // Apply the upgrade by adding the new pet to the player's party.
    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();

        // Add the new pet to the player's party using the PartyManager
        PartyManager.Instance.AddToParty(newPetPrefab);
    }
}
