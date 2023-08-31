using UnityEngine;

// Used by AudioManager to create an array of sounds in the Unity inspector.
[System.Serializable]
public class Sound
{

    public string name;  // Name to be used when called from other scripts.
    public AudioClip clip;  // The audio file itself.

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    // Creates an AudioSource component for this sound.
    // AudioManager object attaches components for each sound to itself via AudioManager script.
    [HideInInspector]
    public AudioSource source;
}
