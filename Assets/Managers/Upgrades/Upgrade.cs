[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public string description;
    public UpgradeType type;
    // Add more attributes as needed

    public virtual void ApplyUpgrade()
    {
        // Apply the upgrade effects to the player's stats
    }
}

public enum UpgradeType
{
    Pet, // For adding new pets
    Damage, // For increasing damage
    Health, // For increasing health
    // Add more upgrade types as needed
}
