using UnityEngine;

[System.Serializable]
public class SynergyUpgrade : Upgrade
{
    public PetType synergyType;

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade(); // Call the base class method

        // Get a reference to the SynergyManager instance
        SynergyManager synergyManager = SynergyManager.Instance;

        // Apply a permanent synergy bonus to the specified synergy type
        synergyManager.ApplyPermanentSynergyBonus(synergyType);

        // Output debug information to the console
        Debug.Log("Synergy upgrade applied: " + synergyType);
    }
}
