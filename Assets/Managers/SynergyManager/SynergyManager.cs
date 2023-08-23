using UnityEngine;
using System.Collections.Generic;

public class SynergyManager : MonoBehaviour
{
    public static SynergyManager Instance { get; private set; }

    [System.Serializable]
    public class SynergyData
    {
        public string synergyName;
        public PetType requiredType;
        public int requiredCount;
        public int bonusValue;
    }

    public List<SynergyData> synergies = new List<SynergyData>();

    private Dictionary<PetType, int> typeCount = new Dictionary<PetType, int>();
    // New dictionary to store permanent synergy bonuses
    private Dictionary<PetType, int> permanentSynergyBonuses = new Dictionary<PetType, int>();

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
            }

            if (petScript != null)
            {
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

            // Apply the permanent bonusValue to the pets or players as needed
            // ...
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
                    // Apply synergy bonus
                    Debug.Log("Synergy activated: " + synergy.synergyName);
                    // Apply the bonusValue to the pets or players as needed
                }
            }
        }
    }

    public void ApplyPermanentSynergyBonus(PetType synergyType)
    {
        // Add or update the permanent synergy bonus in the dictionary
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
