using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Start with a fade-in when the fader is instantiated
        FadeIn();
    }

    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeTo(0.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(1.0f));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
    }
}
