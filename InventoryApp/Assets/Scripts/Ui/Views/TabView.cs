using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TabView : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeSpeed = 10;

    private const float OPENFADE = 1;
    private const float CLOSEFADE = 0;

    private bool fading = false;

    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        if (!fading)
        {
            StartCoroutine(Fade(OPENFADE));
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Close()
    {
        if (!fading)
        {
            StartCoroutine(Fade(CLOSEFADE));
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private IEnumerator Fade(float endAlpha)
    {
        fading = true;
        float beginAlpha = canvasGroup.alpha;
        float direction = beginAlpha > endAlpha ? -1 : 1;
        float time = 0;


        while (canvasGroup.alpha != endAlpha)
        {
            float step = fadeSpeed * Time.deltaTime;

            time += step;

            canvasGroup.alpha = Mathf.Lerp(beginAlpha, endAlpha, time);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        fading = false;
    }
}
