using UnityEngine;
using System.Collections.Generic;

public class SynergyManager : MonoBehaviour
{
    // Singleton instance
    public static SynergyManager Instance { get; private set; }

    // Data structure to hold synergy information
    [System.Serializable]
    public class SynergyData
    {
        public string synergyName;
        public PetType requiredType;
        public int requiredCount;
        public int bonusValue;
    }

    // List of defined synergies
    public List<SynergyData> synergies = new List<SynergyData>();

    // Dictionary to track the count of each pet type in the party
    private Dictionary<PetType, int> typeCount = new Dictionary<PetType, int>();

    // Dictionary to store permanent synergy bonuses
    private Dictionary<PetType, int> permanentSynergyBonuses = new Dictionary<PetType, int>();

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update the synergies based on the current party configuration
    public void UpdateSynergies(List<GameObject> party)
    {
        // Clear type count dictionary
        typeCount.Clear();
        Debug.Log("Clearing type count.");

        // Count the number of each type in the party
        foreach (GameObject pet in party)
        {
            Pet petScript = pet.GetComponent<Pet>();
            if (petScript != null)
            {
                if (typeCount.ContainsKey(petScript.type1))
                {
                    typeCount[petScript.type1]++;
                }
                else
                {
                    typeCount.Add(petScript.type1, 1);
                }

                if (typeCount.ContainsKey(petScript.type2))
                {
                    typeCount[petScript.type2]++;
                }
                else
                {
                    typeCount.Add(petScript.type2, 1);
                }
            }
        }

        // Apply permanent synergy bonuses
        foreach (var entry in permanentSynergyBonuses)
        {
            PetType synergyType = entry.Key;
            int bonusValue = entry.Value;

            if (typeCount.ContainsKey(synergyType))
            {
                typeCount[synergyType] += bonusValue;
            }
        }

        // Apply synergies based on type count
        foreach (SynergyData synergy in synergies)
        {
            if (typeCount.TryGetValue(synergy.requiredType, out int petCountOfType))
            {
                if (petCountOfType >= synergy.requiredCount)
                {
                    Debug.Log("Synergy activated: " + synergy.synergyName);
                }
            }
        }
    }

    // Apply a permanent synergy bonus to a specific type
    public void ApplyPermanentSynergyBonus(PetType synergyType)
    {
        if (permanentSynergyBonuses.ContainsKey(synergyType))
        {
            permanentSynergyBonuses[synergyType]++;
        }
        else
        {
            permanentSynergyBonuses.Add(synergyType, 1);
        }

        Debug.Log("Permanent synergy bonus applied: " + synergyType + " +1.");
        Debug.Log("New value of " + synergyType + " is: " + permanentSynergyBonuses[synergyType]);

        UpdateSynergies(PartyManager.Instance.party);
    }
}
