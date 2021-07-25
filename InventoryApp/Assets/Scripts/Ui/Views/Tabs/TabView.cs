using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class TabView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    private float fadeSpeed = 10;

    private const float OPENFADE = 1;
    private const float CLOSEFADE = 0;

    private bool fading = false;

    [SerializeField]
    private Button optionsButton = null;
    [SerializeField]
    private Button removeButton = null;
    [SerializeField]
    protected OptionsMenu optionsMenu = null;

    [SerializeField]
    protected Identifier addIdentifier = null;

    [SerializeField]
    protected Identifier confirmationIdentifier = null;

    [SerializeField]
    protected Identifier editIdentifier = null;



    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (optionsButton != null)
            optionsButton.onClick.AddListener(OpenOptions);
        if (removeButton != null)
        {
            removeButton.onClick.AddListener(RemoveButtonClick);
            removeButton.gameObject.SetActive(false);
        }
        this.optionsMenu.onStateChanged.AddListener(StateChanged);
    }

    public virtual void Open()
    {
        if (!fading)
        {
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
            this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
        }
    }

    private void OpenOptions()
    {
        optionsMenu.Open();
    }

    public OptionsMenu GetOptionsMenu()
    {
        return optionsMenu;
    }

    private void StateChanged(OptionsMenu.OptionsState state)
    {

        switch (state)
        {
            case OptionsMenu.OptionsState.Add:
                SetAddState();
                break;
            case OptionsMenu.OptionsState.Edit:
                SetEditState();
                break;
            case OptionsMenu.OptionsState.None:
                SetNoneState();
                break;
            case OptionsMenu.OptionsState.Remove:
                SetRemoveState(true);
                break;
        }
    }

    protected virtual void SetNoneState()
    {
        removeButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(true);
    }

    protected virtual void SetAddState() { }

    protected virtual void SetRemoveState(bool value) 
    {
        removeButton.gameObject.SetActive(value);
        optionsButton.gameObject.SetActive(!value);
    }

    protected virtual void SetEditState() { }

    protected virtual void RemoveButtonClick() { }

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
