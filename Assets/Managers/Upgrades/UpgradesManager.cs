using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public List<NewPetUpgrade> newPetUpgrades = new List<NewPetUpgrade>();
    public List<UpgradeCurrentPet> currentPetUpgrades = new List<UpgradeCurrentPet>();

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

    public Upgrade GetRandomUpgrade()
    {
        // Set only pets on first wave
        WaveManager waveManager = FindAnyObjectByType<WaveManager>();
        if (waveManager.currentWave == 0)
        {
            Debug.Log("First wave!");
            return GetRandomPet();
        } else
        {
            int upgradeTypeIndex = Random.Range(0, 3);
            switch (upgradeTypeIndex)
            {
                case 0:
                    Debug.Log("Type 0.");
                    return GetRandomPet();
                case 1:
                    Debug.Log("Type 1.");
                    return UpgradeExistingPet();
                case 2:
                    Debug.Log("Type 2.  Rerolling.");
                    return GetRandomUpgrade();
                default:
                    return GetRandomUpgrade();
            }
        }
    }

    private Upgrade GetRandomPet()
    {
        int randomIndex = Random.Range(0, newPetUpgrades.Count);
        return newPetUpgrades[randomIndex];
    }

    private Upgrade UpgradeExistingPet()
    {
        int randomIndex = Random.Range(0, currentPetUpgrades.Count);
        return currentPetUpgrades[randomIndex].InitializeUpgrade();
    }
}
