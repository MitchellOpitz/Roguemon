using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    public GameObject startingPet;  // Remove later.
    public GameObject pet2;  // Remove later.
    public GameObject pet3;  // Remove later.
    public GameObject pet4;  // Remove later.
    public Vector3 startingSpawn;  // Remove later.


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

        InitializePetParty(startingPet);  // Later, this will be done from the Pet Selection menu at the start of the game.
        TestParty();
    }

    private void TestParty()
    {
        StartCoroutine(AddStartingPetWithDelay(10f, pet2));
        StartCoroutine(AddStartingPetWithDelay(20f, pet3));
        StartCoroutine(AddStartingPetWithDelay(30f, pet4));
    }

    private IEnumerator AddStartingPetWithDelay(float time, GameObject pet)
    {
        yield return new WaitForSeconds(time);
        AddToParty(pet);
    }

    public void InitializePetParty(GameObject selectedPet)
    {
        GameObject starterPet = Instantiate(selectedPet, startingSpawn, Quaternion.identity);

        // Initialize party list
        party.Clear();
        party.Add(starterPet);

        // Initialize UI for pet party
        UpdatePartyMovement();
    }

    public void AddToParty(GameObject pet)
    {
        if (party.Count > 0)
        {
            // Calculate spawn position behind the last member of the party
            Vector3 spawnDirection = -party[party.Count - 1].transform.forward;
            Vector3 spawnPosition = party[party.Count - 1].transform.position + spawnDirection * 1.5f;

            // Instantiate the new pet behind the last party member
            GameObject newPet = Instantiate(pet, spawnPosition, Quaternion.identity);
            newPet.transform.forward = transform.forward;

            // Add the new pet to the party list
            party.Add(newPet);

            UpdatePartyMovement();

            synergyManager.UpdateSynergies(party);
        }
    }

    private void UpdatePartyMovement()
    {
        for (int i = 0; i < party.Count; i++)
        {
            PlayerMovement playerMovement = party[i].GetComponent<PlayerMovement>();
            Follow follow = party[i].GetComponent<Follow>();

            if (i == 0)
            {
                //Debug.Log("Updating Lead Pet.");
                // Lead pet
                playerMovement.enabled = true;
                follow.enabled = false;
                follow.target = null;
            }
            else
            {
                // Follower pets
                //Debug.Log("Updating Follower Pet " + i + ".");
                playerMovement.enabled = false;
                follow.enabled = true;
                follow.target = party[i - 1].transform;
            }
        }
    }

    public void PetDied(GameObject deadPet)
    {
        party.Remove(deadPet);
        synergyManager.UpdateSynergies(party);
        UpdatePartyMovement();
    }

    public bool CheckRoomForPets()
    {
        if (party.Count < 6)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
