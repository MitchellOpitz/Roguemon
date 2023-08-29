using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalMenuManager : MonoBehaviour 
{
    [SerializeField]
    private RectTransform optionsMenu;
    [SerializeField]
    private RectTransform creditsMenu;
    [SerializeField] 
    private RectTransform quitMenu;
    [SerializeField]
    private Vector2 offScreenMenuDockRight;
    [SerializeField]
    private Vector2 offScreenMenuDockBottom;

    private void Start() {
        optionsMenu.anchoredPosition = offScreenMenuDockRight;
        creditsMenu.anchoredPosition = offScreenMenuDockRight;
        quitMenu.anchoredPosition = offScreenMenuDockBottom;
    }

    public void LoadOptionsMenu() {
        StartCoroutine(BringOnScreen(optionsMenu));
        StartCoroutine(TakeOffScreen(creditsMenu, offScreenMenuDockRight));
        StartCoroutine(TakeOffScreen(quitMenu, offScreenMenuDockBottom));
    }
    public void LoadCreditsMenu() {
        StartCoroutine(BringOnScreen(creditsMenu));
        StartCoroutine(TakeOffScreen(optionsMenu, offScreenMenuDockRight));
        StartCoroutine(TakeOffScreen(quitMenu, offScreenMenuDockBottom));
    }
    public void LoadQuitMenu() {
        StartCoroutine(BringOnScreen(quitMenu));
        StartCoroutine(TakeOffScreen(optionsMenu, offScreenMenuDockRight));
        StartCoroutine(TakeOffScreen(creditsMenu, offScreenMenuDockRight));
    }
    public void UploadAllMenus() {
        StartCoroutine(TakeOffScreen(optionsMenu, offScreenMenuDockRight));
        StartCoroutine(TakeOffScreen(creditsMenu, offScreenMenuDockRight));
        StartCoroutine(TakeOffScreen(quitMenu, offScreenMenuDockBottom));
    }

    IEnumerator BringOnScreen(RectTransform menu) {
        float t = 0f;
        Vector2 currentLocation = menu.anchoredPosition;
        while (t < 0.5f) {
            t += Time.deltaTime;
            menu.anchoredPosition = Vector2.Lerp(currentLocation, new Vector2(0,0), t*2);
            yield return null;
        }
    }
    IEnumerator TakeOffScreen(RectTransform menu, Vector2 offScreenTarget) {
        float t = 0f;
        Vector2 currentLocation = menu.anchoredPosition;
        while (t < 0.5f) {
            t += Time.deltaTime;
            menu.anchoredPosition = Vector2.Lerp(currentLocation, offScreenTarget, t*2);
            yield return null;
        }
    }
}
