using UnityEngine;

public class Clamp : MonoBehaviour
{
    [Header("Clamp Boundaries")]
    public float minX = -18.5f;
    public float maxX = 9.5f;
    public float minZ = -11f;
    public float maxZ = 7f;

    // Singleton pattern to make it easily accessible from other scripts
    public static Clamp Instance { get; private set; }

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
}
