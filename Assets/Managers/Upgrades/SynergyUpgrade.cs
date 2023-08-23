using UnityEngine;

[System.Serializable]
public class SynergyUpgrade : Upgrade
{
    public PetType synergyType;

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade(); // Call the base class method

        // Access the PartyManager to manage party synergies
        SynergyManager synergyManager = SynergyManager.Instance;

        // Upgrade the chosen synergy by +1
        synergyManager.ApplyPermanentSynergyBonus(synergyType);

        // Output debug information
        Debug.Log("Synergy upgrade button clicked.");
    }
}
