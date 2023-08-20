using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    public GameObject startingPet;  // Remove later.
    public Vector3 startingSpawn;  // Remove later.

    public List<GameObject> party = new List<GameObject>();


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
        for (int i = 01; i <= 5; i++)
        {
            StartCoroutine(AddStartingPetWithDelay(i * 3f));
        }
    }

    private IEnumerator AddStartingPetWithDelay(float time)
    {
        yield return new WaitForSeconds(time); // Wait for 3 seconds
        AddToParty(startingPet);
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
                Debug.Log("Updating Lead Pet.");
                // Lead pet
                playerMovement.enabled = true;
                follow.enabled = false;
                follow.target = null;
            }
            else
            {
                // Follower pets
                Debug.Log("Updating Follower Pet " + i + ".");
                playerMovement.enabled = false;
                follow.enabled = true;
                follow.target = party[i - 1].transform;
            }
        }
    }

    public void PetDied(GameObject deadPet)
    {
        party.Remove(deadPet);
        UpdatePartyMovement();
    }
}
