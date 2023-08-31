using System;
using UnityEngine;

// To call a sound, use the following within a script:
// FindObjectOfType<AudioManager>().PlaySound("NAME");
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public Sound[] sounds;
    public bool testAudio;

    void Awake()
    {
        // Checks if an instance of the AudioManager already exists in a scene.
        // Used when transitioning between scenes to avoid restarting music.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Attaches AudioSource component from each sound to the AudioManager game object.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            if (testAudio)
            {
                PlaySound(sound.name);
            }
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound not found: " + name + ".");
            return;
        }
        sound.source.Play();
    }
}
