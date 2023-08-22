using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesUI : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    public bool isUpgradeMenuOpen;

    private void Start()
    {
        upgradeMenuUI.SetActive(false);
    }

    public void ShowUpgradeMenu()
    {
        // Show the upgrade menu UI
        upgradeMenuUI.SetActive(true);
        isUpgradeMenuOpen = true;

        // Randomly choose 3 upgrades
        for (int i = 0; i < 3; i++)
        {
            Upgrade randomUpgrade = UpgradeManager.Instance.GetRandomUpgrade();
            UpdateTitle(i, randomUpgrade.upgradeName);
            UpdateDescription(i, randomUpgrade.description);
            UpdateButton(i, randomUpgrade);
        }
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
        Debug.Log("Title: " + text);
    }

    private void UpdateDescription(int upgradeIndex, string text)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Transform description = upgradeBlock.GetChild(1);
        TextMeshProUGUI descriptionText = description.GetComponent<TextMeshProUGUI>();

        // Update the title text
        if (descriptionText != null)
        {
            descriptionText.text = text;
        }
        Debug.Log("Description: " + text);
    }

    private void UpdateButton(int upgradeIndex, Upgrade upgrade)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Button button = upgradeBlock.GetChild(2).GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnUpgradeOptionClicked(upgrade));
    }

    private void OnUpgradeOptionClicked(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade(); // Apply the selected upgrade
        isUpgradeMenuOpen = false;
        upgradeMenuUI.SetActive(false); // Hide the upgrade menu UI
    }
}
