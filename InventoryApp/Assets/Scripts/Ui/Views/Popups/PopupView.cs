using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopupView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private bool fading = false;

    private const int OPENFADE = 1;
    private const int CLOSEFADE = 0;

    private float fadeSpeed = 10;

    [SerializeField]
    private Identifier identifier = null;

    [SerializeField]
    private Button closeButton = null;

    public Identifier Identifier
    {
        get
        {
            return identifier;
        }
    }

    protected virtual void Awake()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
        if(identifier == null)
        {
            Debug.LogError("Identifier is null, no way to acces this popup " + this.gameObject.name);
        }

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseButtonClick);
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

    private void CloseButtonClick()
    {
        PopupManager.instance.ClosePopup();
    }
}
