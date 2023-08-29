using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    public string playerTag = "Player";
    public float healingRate = 10f; // Amount of healing per second

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Heal the player over time
            Pet pet = other.GetComponent<Pet>();
            if (pet != null)
            {
                float healAmount = healingRate * Time.deltaTime;
                // pet.Heal(healAmount);
                // Debug.Log("Healing for: " + healAmount);
            }
        }
    }
}