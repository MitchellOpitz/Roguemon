using UnityEngine;
using System.Collections.Generic;

// Manages the player's pet party and related interactions.
public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }
    public Vector3 startingSpawn;  // Remove later.
    public int maxPartySize = 6;

    public List<GameObject> party = new List<GameObject>();
    public SynergyManager synergyManager;

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

    // Initializes the player's pet party with the selected pet.
    public void InitializePetParty(GameObject selectedPet)
    {
        GameObject starterPet = Instantiate(selectedPet, startingSpawn, Quaternion.identity);

        // Initialize party list
        party.Clear();
        party.Add(starterPet);

        // Initialize UI for pet party
        UpdatePartyMovement();
    }

    // Adds a pet to the player's party.
    public void AddToParty(GameObject pet)
    {
        if (party.Count == 0)
        {
            InitializePetParty(pet);
        }
        else
        {
            // Calculate spawn position behind the last member of the party
            Vector3 spawnDirection = -party[party.Count - 1].transform.forward;
            Vector3 spawnPosition = party[party.Count - 1].transform.position + spawnDirection * 1.5f;

            // Instantiate the new pet behind the last party member
            GameObject newPet = Instantiate(pet, spawnPosition, Quaternion.identity);
            newPet.transform.forward = transform.forward;
            newPet.name = newPet.name.Replace("(Clone)", "");


            // Add the new pet to the party list
            party.Add(newPet);

            UpdatePartyMovement();

            synergyManager.UpdateSynergies(party);
        }
    }

    // Updates the movement behavior for the pets in the party.
    private void UpdatePartyMovement()
    {
        for (int i = 0; i < party.Count; i++)
        {
            PlayerMovement playerMovement = party[i].GetComponent<PlayerMovement>();
            Follow follow = party[i].GetComponent<Follow>();

            if (i == 0)
            {
                // Lead pet
                playerMovement.enabled = true;
                follow.enabled = false;
                follow.target = null;
            }
            else
            {
                // Follower pets
                playerMovement.enabled = false;
                follow.enabled = true;
                follow.target = party[i - 1].transform;
            }
        }
    }

    // Handles the removal of a dead pet from the party.
    public void PetDied(GameObject deadPet)
    {
        party.Remove(deadPet);
        synergyManager.UpdateSynergies(party);
        UpdatePartyMovement();
    }

    // Checks if there is room for more pets in the party.
    public bool CheckRoomForPets()
    {
        return party.Count < maxPartySize;
    }
}
