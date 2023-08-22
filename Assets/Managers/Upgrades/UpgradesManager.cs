using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public List<NewPetUpgrade> newPetUpgrades = new List<NewPetUpgrade>();

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
        int randomIndex = Random.Range(0, newPetUpgrades.Count);
        return newPetUpgrades[randomIndex];
    }
}
