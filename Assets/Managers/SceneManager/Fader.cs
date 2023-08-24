using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Manages fading effects using a UI Image component.
public class Fader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    // Start with a fade-in when the fader is instantiated.
    private void Start()
    {
        FadeIn();
    }

    // Initiates a fade-in effect.
    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeTo(0.0f));
    }

    // Initiates a fade-out effect.
    public void FadeOut()
    {
        StartCoroutine(FadeTo(1.0f));
    }

    // Performs the fading animation.
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
