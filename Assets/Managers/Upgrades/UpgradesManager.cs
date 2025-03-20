using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public List<NewPetUpgrade> newPetUpgrades = new List<NewPetUpgrade>();
    public List<UpgradeCurrentPet> currentPetUpgrades = new List<UpgradeCurrentPet>();
    public List<SynergyUpgrade> synergyUpgrades = new List<SynergyUpgrade>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Retrieves a random upgrade based on the game's progression.
    public Upgrade GetRandomUpgrade()
    {
        // Check if it's the first wave to decide which upgrades are available
        WaveManager waveManager = FindAnyObjectByType<WaveManager>();
        if (waveManager.CurrentWave == 0)
        {
            return GetRandomPetUpgrade();
        }
        else
        {
            int upgradeTypeIndex = Random.Range(0, 3);

            PartyManager partyManager = PartyManager.Instance;
            if (upgradeTypeIndex == 0 && partyManager.party.Count == partyManager.maxPartySize)
            {
                return GetRandomUpgrade();
            }

            // Selects an upgrade based on the chosen type index
            switch (upgradeTypeIndex)
            {
                case 0:
                    return GetRandomPetUpgrade();
                case 1:
                    return UpgradeExistingPet();
                case 2:
                    return GetSynergyUpgrade();
                default:
                    return GetRandomUpgrade();
            }
        }
    }

    // Retrieves a random new pet upgrade.
    private Upgrade GetRandomPetUpgrade()
    {
        int randomIndex = Random.Range(0, newPetUpgrades.Count);
        return newPetUpgrades[randomIndex];
    }

    // Retrieves an upgrade to improve an existing pet.
    private Upgrade UpgradeExistingPet()
    {
        int randomIndex = Random.Range(0, currentPetUpgrades.Count);
        return currentPetUpgrades[randomIndex].InitializeUpgrade();
    }

    // Retrieves a random synergy upgrade.
    private Upgrade GetSynergyUpgrade()
    {
        int randomIndex = Random.Range(0, synergyUpgrades.Count);
        return synergyUpgrades[randomIndex];
    }
}
