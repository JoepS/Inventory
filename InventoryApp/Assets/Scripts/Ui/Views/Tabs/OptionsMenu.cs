using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour, IPointerClickHandler
{
    private CanvasGroup canvasGroup;

    public OptionsState currentState = OptionsState.None;

    [SerializeField]
    private Button addButton = null;

    [SerializeField]
    private Button removeButton = null;

    [SerializeField]
    private Button editButton = null;

    public OptionsStateEvent onStateChanged = new OptionsStateEvent();

    public enum OptionsState
    {
        None,
        Add,
        Edit,
        Remove
    }
    
    void Awake()
    {
        addButton.onClick.AddListener(AddButtonClick);
        removeButton.onClick.AddListener(RemoveButtonClick);
        editButton.onClick.AddListener(EditButtonClick);
        canvasGroup = this.GetComponent<CanvasGroup>();
        Close();
    }

    public void AddButtonClick()
    {
        SetState(OptionsState.Add);
        Close();
    }
    public void RemoveButtonClick()
    {
        SetState(OptionsState.Remove);
        Close();
    }
    public void EditButtonClick()
    {
        SetState(OptionsState.Edit);
        Close();
    }

    public void SetState(OptionsState state)
    {
        Debug.Log("Set state " + state);
        currentState = state;
        onStateChanged.Invoke(state);
    }

    public OptionsState GetCurrentState()
    {
        return currentState;
    }

	public void Open()
	{
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
	}

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}

public class OptionsStateEvent : UnityEvent<OptionsMenu.OptionsState> { }
