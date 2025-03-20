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

    // Show the upgrade menu UI and populate it with random upgrade options.
    public void ShowUpgradeMenu()
    {
        upgradeMenuUI.SetActive(true);
        isUpgradeMenuOpen = true;

        // Populate the upgrade menu with random upgrades
        for (int i = 0; i < 3; i++)
        {
            Upgrade randomUpgrade = UpgradeManager.Instance.GetRandomUpgrade();
            UpdateTitle(i, randomUpgrade.upgradeName);
            UpdateDescription(i, randomUpgrade.description);
            UpdateButton(i, randomUpgrade);
        }
    }

    // Update the title text of a specific upgrade block.
    private void UpdateTitle(int upgradeIndex, string text)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Transform title = upgradeBlock.GetChild(0);
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();

        if (titleText != null)
        {
            titleText.text = text;
        }
    }

    // Update the description text of a specific upgrade block.
    private void UpdateDescription(int upgradeIndex, string text)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Transform description = upgradeBlock.GetChild(1);
        TextMeshProUGUI descriptionText = description.GetComponent<TextMeshProUGUI>();

        if (descriptionText != null)
        {
            descriptionText.text = text;
        }
    }

    // Update the button behavior of a specific upgrade block.
    private void UpdateButton(int upgradeIndex, Upgrade upgrade)
    {
        Transform upgradeBlock = GameObject.Find("Upgrade" + (upgradeIndex + 1).ToString()).GetComponent<Transform>();
        Button button = upgradeBlock.GetChild(2).GetComponent<Button>();

        // Set the button's behavior to apply the selected upgrade on click
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnUpgradeOptionClicked(upgrade));
    }

    // Apply the selected upgrade and close the upgrade menu UI.
    private void OnUpgradeOptionClicked(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade();
        isUpgradeMenuOpen = false;
        upgradeMenuUI.SetActive(false);
    }
}
