using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    public List<GameObject> startingPets = new List<GameObject>();


    /*
    public Button[] upgradeButtons; // Assign in the Inspector

    public GameObject petOptionPrefab; // Prefab for pet option
    public Transform petOptionParent; // Parent for pet options
    public GameObject upgradeOptionPrefab; // Prefab for upgrade option
    public Transform upgradeOptionParent; // Parent for upgrade options
    */

    private void Start()
    {
        ShowUpgradeMenu(true, true);
    }

    public void ShowUpgradeMenu(bool isFirstWave, bool hasRoomForPets)
    {
        upgradeMenuUI.SetActive(true); // Show the upgrade menu

        // Populate options
        if (isFirstWave)
        {
            // Show random pet options
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, startingPets.Count);
                UpdateTitle(i, startingPets[randomIndex].GetComponent<Pet>().name);
                UpdateDescription(i, startingPets[randomIndex].GetComponent<Pet>().type1.ToString());
                // Attach logic to the petOptionPrefab UI elements
                // For example: petOptionPrefab.GetComponent<Button>().onClick.AddListener(() => OnPetOptionClicked(petOptionPrefab));
            }
        }
        else if (hasRoomForPets)
        {
            // Show pet option and upgrade options
            // Instantiate petOptionPrefab for pet option
            // Instantiate upgradeOptionPrefab for each upgrade option
            // Assign data to UI elements
        }
        else
        {
            // Show only upgrade options
            // Instantiate upgradeOptionPrefab for each upgrade option
            // Assign data to UI elements
        }

        Time.timeScale = 0;
    }

    private void UpdateTitle(int upgradeIndex, string text)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Transform title = upgradeBlock.GetChild(0);
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();

        // Update the title text
        if (titleText != null)
        {
            titleText.text = text;
        }
    }

    private void UpdateDescription (int upgradeIndex, string text)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Transform title = upgradeBlock.GetChild(1);
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();

        // Update the title text
        if (titleText != null)
        {
            titleText.text = text;
        }
    }

    public void OnUpgradeOptionClicked(int upgradeIndex)
    {
        // Implement logic for applying the selected upgrade
        // Update the player's party, stats, etc. based on the upgrade
        // Close the upgrade menu
        Time.timeScale = 1;
        upgradeMenuUI.SetActive(false);
    }
}
