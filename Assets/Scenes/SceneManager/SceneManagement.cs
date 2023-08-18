using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    public Fader fader; // Reference to the Fader script

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

    public void LoadSceneByName(string sceneName)
    {
        fader.FadeOut();
        StartCoroutine(LoadSceneWithDelay(sceneName));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(fader.fadeDuration); // Wait for fade-out to complete
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
