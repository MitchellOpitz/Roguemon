using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundWiggle : MonoBehaviour
{
    public float ditherBackgroundWiggleRange;
    public Image ditheredBackgroundArt;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DitherBackgroundWiggle(ditherBackgroundWiggleRange));
    }
    IEnumerator DitherBackgroundWiggle(float rotationRange)
    {

        while (true)
        {
            ditheredBackgroundArt.transform.localRotation = Quaternion.Euler(0, 0, (Mathf.PingPong(Time.time, rotationRange) - (rotationRange / 2)));
            yield return null;
        }

    }
}
