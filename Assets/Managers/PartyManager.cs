using UnityEngine;
using System.Collections;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    public GameObject startingPet;  // Remove later.
    public Vector3 startingSpawn;  // Remove later.

    public GameObject[] party;

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
        //TestParty();
    }

    private void TestParty()
    {
        for (int i = 01; i <= 10; i++)
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

        // Initialize followerPets array
        GameObject[] newParty = new GameObject[1];
        newParty[0] = starterPet;
        party = newParty;

        // Initialize UI for pet party
        UpdatePartyMovement();
    }

    public void AddToParty(GameObject pet)
    {
        // Calculate spawn position behind the last member of the party
        Vector3 spawnDirection = -party[party.Length - 1].transform.forward;
        //Debug.Log("Spawn Direction: " + spawnDirection);
        Vector3 spawnPosition = party[party.Length - 1].transform.position + spawnDirection * 1.5f;
        //Debug.Log("Spawn Position: " + spawnPosition);

        // Instantiate the new pet behind the last party member
        GameObject newPet = Instantiate(pet, spawnPosition, Quaternion.identity);
        newPet.transform.forward = transform.forward;

        // Update the party array to include the new pet
        GameObject[] newParty = new GameObject[party.Length + 1];
        party.CopyTo(newParty, 0);
        newParty[newParty.Length - 1] = newPet;
        party = newParty;

        UpdatePartyMovement();
    }

    private void UpdatePartyMovement()
    {
        for (int i = 0; i < party.Length; i++)
        {
            PlayerMovement playerMovement = party[i].GetComponent<PlayerMovement>();
            Follow follow = party[i].GetComponent<Follow>();

            if (i == 0)
            {
                // Debug.Log("Updating Lead Pet.");
                // Lead pet
                playerMovement.enabled = true;
                follow.enabled = false;
                follow.target = null;
            }
            else
            {
                // Follower pets
                // Debug.Log("Updating Follower Pet " + i + ".");
                playerMovement.enabled = false;
                follow.enabled = true;
                follow.target = party[i - 1].transform;
            }
        }
    }
}
