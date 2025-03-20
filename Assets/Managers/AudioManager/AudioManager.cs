using System;
using UnityEngine;
using System.Collections;
using TMPro;

// To call a sound, use the following within a script:
// FindObjectOfType<AudioManager>().PlaySound("NAME");
public class AudioManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public static AudioManager instance;
    public Sound[] sounds;

    public int currentAudioTrackIndex; // Default track index
    public string trackToStartPlaying;

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
        //DontDestroyOnLoad(gameObject);

        // Attaches AudioSource component from each sound to the AudioManager game object.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        // Load the saved audio track preference
        currentAudioTrackIndex = PlayerPrefs.GetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
        PlayAudioTrack(currentAudioTrackIndex);

        StopAllAudio();
        PlaySound(trackToStartPlaying);
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

    public void PlayAudioTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < sounds.Length)
        {
            StopAllAudio();
            currentAudioTrackIndex = trackIndex;
            PlaySound(sounds[currentAudioTrackIndex].name);

            // Save the current audio track preference
            PlayerPrefs.SetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
        }
    }

    private void StopAllAudio()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public void TransitionToAudioTrack(int trackIndex)
    {
        StartCoroutine(FadeOutAndChangeTrack(trackIndex));
    }

    private IEnumerator FadeOutAndChangeTrack(int newTrackIndex)
    {
        float fadeDuration = 2.0f; // Duration of fade-out
        float timer = 0f;
        float startVolume = sounds[currentAudioTrackIndex].source.volume;

        while (timer < fadeDuration)
        {
            float normalizedTime = timer / fadeDuration;
            float currentVolume = Mathf.Lerp(startVolume, 0f, normalizedTime);
            sounds[currentAudioTrackIndex].source.volume = currentVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        sounds[currentAudioTrackIndex].source.Stop();
        currentAudioTrackIndex = newTrackIndex;
        PlaySound(sounds[currentAudioTrackIndex].name);

        StartCoroutine(FadeInNewTrack());
    }

    private IEnumerator FadeInNewTrack()
    {
        float fadeDuration = 2.0f; // Duration of fade-in
        float timer = 0f;
        float startVolume = sounds[currentAudioTrackIndex].source.volume;

        sounds[currentAudioTrackIndex].source.volume = 0f;
        sounds[currentAudioTrackIndex].source.Play();

        while (timer < fadeDuration)
        {
            float normalizedTime = timer / fadeDuration;
            float currentVolume = Mathf.Lerp(0f, startVolume, normalizedTime);
            sounds[currentAudioTrackIndex].source.volume = currentVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        sounds[currentAudioTrackIndex].source.volume = startVolume;

        // Update the dropdown value to reflect the selected track
        dropdown = FindObjectOfType<TMP_Dropdown>();
        dropdown.value = currentAudioTrackIndex;

        // Save the current audio track preference
        PlayerPrefs.SetInt("CurrentAudioTrackIndex", currentAudioTrackIndex);
    }
}