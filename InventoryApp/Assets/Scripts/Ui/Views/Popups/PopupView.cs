using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopupView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private bool fading = false;

    private const int OPENFADE = 1;
    private const int CLOSEFADE = 0;

    private float fadeSpeed = 10;

    protected UnityEvent onClose = new UnityEvent();

    [SerializeField]
    private Identifier identifier = null;

    [SerializeField]
    private Button closeButton = null;

    [SerializeField]
    private GameObject formContent = null;

    protected List<SelectableValue> selectables;

    public Identifier Identifier
    {
        get
        {
            return identifier;
        }
    }

    protected virtual void Awake()
    {
        if(this.canvasGroup == null)
            this.canvasGroup = this.GetComponent<CanvasGroup>();
        if(identifier == null)
        {
            Debug.LogError("Identifier is null, no way to acces this popup " + this.gameObject.name);
        }
        
        if(formContent != null)
            selectables = formContent.GetComponentsInChildren<SelectableValue>().ToList();

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseButtonClick);
    }

    public virtual void Open(object[] optionals)
    {
        if (!fading)
        {
            onClose.RemoveAllListeners();
            StartCoroutine(Fade(OPENFADE));
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public virtual void Close()
    {
        if (!fading)
        {
            StartCoroutine(Fade(CLOSEFADE));
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            ClearInputs();
            onClose.Invoke();
        }
    }

    private IEnumerator Fade(float endAlpha)
    {
        if (canvasGroup == null)
            yield break;
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
    protected SelectableValue GetFromIdentifier(Identifier identifier)
    {
        foreach (SelectableValue selectableValue in selectables)
        {
            if (selectableValue.identifier.Equals(identifier))
            {
                return selectableValue;
            }
        }
        return null;
    }
    private void ClearInputs()
    {
        if (selectables == null)
            return;
        foreach (SelectableValue sv in selectables)
        {
            sv.Clear();
        }
    }
}
