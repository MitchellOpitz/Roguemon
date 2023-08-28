using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class TitleScreenBackgroundArtManager : MonoBehaviour
{

    [SerializeField]
    private RectTransform lightPinkEntranceBlock;
    [SerializeField]
    private RectTransform ditheredBackgroundArt;
    [SerializeField] 
    private RectTransform titleScreenButtonPanel;
    [SerializeField]
    private RectTransform titleTextbox;
    [SerializeField]
    private float canvasWidth;
    [SerializeField]
    private float canvasHeight;

    [Header("Animation Delays")]
    [SerializeField]
    private float lightPinkEntranceDelay;
    [SerializeField]
    private AnimationCurve lightPinkEntranceCurve;
    [SerializeField]
    private float lightPinkHorizontalStartDelay;
    [SerializeField]
    private AnimationCurve lightPinkHorizontalStartCurve;
    [SerializeField]
    private float lightPinkVerticalStartDelay;
    [SerializeField]
    private AnimationCurve lightPinkVerticalStartCurve;
    [SerializeField]
    private float backgroundShiftStartDelay;
    [SerializeField]
    private AnimationCurve backgroundShiftStartCurve;

    [Header("Title Screen Variables")]
    [SerializeField]
    private float ditherBackgroundWiggleRange;
    [SerializeField]
    private Vector2 UIButtonDestination;
    [SerializeField]
    private Vector2 titleDestination;


    private bool isWiggling;
    private bool startAnimationComplete;
    private bool buttonsOnScreen;
    private bool pinkTransitionComplete;
    private bool titleTransitionCompelte;

    void Start()
    {
        isWiggling = false;
        pinkTransitionComplete= false;
        buttonsOnScreen= false;
        titleTransitionCompelte = false;
        lightPinkEntranceBlock.sizeDelta = new Vector2(10, 10);
        lightPinkEntranceBlock.position = new Vector2(-1200,0);
        ditheredBackgroundArt.sizeDelta = new Vector2(canvasWidth * 1.2f, canvasHeight * 1.2f);
        ditheredBackgroundArt.position = new Vector2(0, -1300);
        titleScreenButtonPanel.position = new Vector2(-1500, 0);
        titleTextbox.position = new Vector2(0, 1000);
        StartCoroutine(LightPinkEntrance(lightPinkEntranceDelay));
        StartCoroutine(LightPinkHorizontalStretch(lightPinkHorizontalStartDelay));
        StartCoroutine(LightPinkVerticalStretch(lightPinkVerticalStartDelay));
        StartCoroutine(BringDitheredBackgroundUp(backgroundShiftStartDelay));
    }

    private void Update() {
        if (startAnimationComplete && !isWiggling) {
            isWiggling = true;
            StartCoroutine(DitherBackgroundWiggle(ditherBackgroundWiggleRange));
        }
        if (pinkTransitionComplete && !buttonsOnScreen) {
            buttonsOnScreen = true;
            StartCoroutine(UIButtonEntranceAnimation());
        }
        if (pinkTransitionComplete && !titleTransitionCompelte) {
            titleTransitionCompelte= true;
            StartCoroutine(TitleEntranceAnimation());
        }
    }


    IEnumerator LightPinkEntrance(float lightPinkEntranceDelay) {
        yield return new WaitForSeconds(lightPinkEntranceDelay);

        float t = 0;
        while (t < 0.5f) {
        lightPinkEntranceBlock.transform.position = Vector2.Lerp(new Vector2(-1200, 0), new Vector2(0, 0), lightPinkEntranceCurve.Evaluate(t * 2));
            t += Time.deltaTime;
            yield return null;
        }

    }
    IEnumerator LightPinkHorizontalStretch(float lightPinkHorizontalStartDelay) {
        yield return new WaitForSeconds(lightPinkHorizontalStartDelay);

        float t = 0;
        while (t < 1f) {
            lightPinkEntranceBlock.sizeDelta = Vector2.Lerp(new Vector2(10, lightPinkEntranceBlock.sizeDelta.y), new Vector2(canvasWidth, lightPinkEntranceBlock.sizeDelta.y), lightPinkHorizontalStartCurve.Evaluate(t));
            t += Time.deltaTime;
            yield return null;
        }

    }
    IEnumerator LightPinkVerticalStretch(float lightPinkVerticalStartDelay) {
        yield return new WaitForSeconds(lightPinkVerticalStartDelay);
        float t = 0;
        while (t < 1f) {
            lightPinkEntranceBlock.sizeDelta = Vector2.Lerp(new Vector2(lightPinkEntranceBlock.sizeDelta.x, 10), new Vector2(lightPinkEntranceBlock.sizeDelta.x, canvasHeight), lightPinkVerticalStartCurve.Evaluate(t));
            t += Time.deltaTime;
            yield return null;
        }
        lightPinkEntranceBlock.sizeDelta = new Vector2(canvasWidth, canvasHeight);
        pinkTransitionComplete = true;
    }

    IEnumerator BringDitheredBackgroundUp(float backgroundShiftStartDelay) {
        yield return new WaitForSeconds(backgroundShiftStartDelay);
        float t = 0;
        while (t < 3f) {
            ditheredBackgroundArt.position = Vector2.Lerp(new Vector2(0, -1300), new Vector2(0, 0), backgroundShiftStartCurve.Evaluate(t/3));
            t += Time.deltaTime;
            yield return null;
        }
        startAnimationComplete = true;
    }

    IEnumerator DitherBackgroundWiggle(float rotationRange) {

        while(true) {
            ditheredBackgroundArt.localRotation = Quaternion.Euler(0,0, (Mathf.PingPong(Time.time, rotationRange)-(rotationRange/2)));
            yield return null;
        }
       
    }

    IEnumerator UIButtonEntranceAnimation() {
        float t = 0;
        while (t < 1f) {
            titleScreenButtonPanel.position = Vector2.Lerp(new Vector2(-1500, 0), UIButtonDestination, t);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TitleEntranceAnimation() {
        float t = 0;
        while (t < 1f) {
            titleTextbox.position = Vector2.Lerp(new Vector2(0,1000), titleDestination, t);
            t += Time.deltaTime;
            yield return null;
        }
    }

}
