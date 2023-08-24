using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

// Manages scene transitions and quitting the game.
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    public Fader fader; // Reference to the Fader script

    // Singleton pattern for managing a single instance of SceneManagement.
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Initiates scene loading by fading out and then starting the scene loading process.
    public void LoadSceneByName(string sceneName)
    {
        fader.FadeOut();
        StartCoroutine(LoadSceneWithDelay(sceneName));
    }

    // Loads the scene with a delay after the fade-out animation.
    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(fader.fadeDuration); // Wait for fade-out to complete
        SceneManager.LoadScene(sceneName);
    }

    // Quits the game.
    public void QuitGame()
    {
        Application.Quit();
    }
}
