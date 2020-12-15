using DataManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class ProductView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    private const float MIN_HOLD_TIME = 1;

    [SerializeField]
    private TMP_Text id = null;

    [SerializeField]
    private TMP_Text productName = null;

    [SerializeField]
    private TMP_Text description = null;

    [SerializeField]
    private TMP_Text cost = null;

    [SerializeField]
    private Button colapsedButton = null;

    [SerializeField]
    private GameObject removeView = null;

    [SerializeField]
    private RectTransform dataView = null;

    [SerializeField]
    private Toggle removeToggle = null;

    public bool GetToggleState
    {
        get
        {
            return removeToggle.isOn;
        }
    }


    private bool colapsed = true;

    public bool removeOpen = false;

    private LayoutElement layoutElement;

    private bool lerping = false;

    private Product product = null;

    private float pointerDownStartTime = 0;

    private OptionsMenu optionsMenu;

    public BoolEvent removeToggleSwitch = new BoolEvent();

    public ProductEvent OnClick = new ProductEvent();


    private void Awake()
    {
        layoutElement = this.GetComponent<LayoutElement>();
        colapsedButton.onClick.AddListener(OnColapsedButtonClick);
        removeToggle.onValueChanged.AddListener(delegate { removeToggleSwitch.Invoke(removeToggle.isOn); });
    }

    public void SetOptionsMenu(OptionsMenu optionsMenu)
    {
        this.optionsMenu = optionsMenu;
    }

	public void Hide()
	{
        this.gameObject.SetActive(false);
	}

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

	private void OnColapsedButtonClick()
    {
        if (removeOpen)
            return;

        if (lerping)
            return;

        float size = layoutElement.minHeight;
        if (colapsed)
        {
            size *= 2;
            colapsed = false;
        }
        else
        {
            size /= 2;
            colapsed = true;
        }
        StartCoroutine(LerpToSize(size));
    }

    private IEnumerator LerpToSize (float endSize)
    {
        lerping = true;
        float startSize = layoutElement.minHeight;
        float time = 0;
        float speed = 4;
        while(layoutElement.minHeight != endSize)
        {
            time += speed * Time.deltaTime;
            layoutElement.minHeight = Mathf.Lerp(startSize, endSize, time);
            yield return null;
        }
        layoutElement.minHeight = endSize;
        lerping = false;
    }

	public void SetProduct(Product product)
	{
        this.product = product;
        UpdateView();
    }
    public Product GetProduct()
    {
        return this.product;
    }

    internal void UpdateProduct(Product product)
	{
        SetProduct(product);
	}

	private void UpdateView()
    {
        this.id.text = product.id + "";
        this.productName.text = product.name;
        this.description.text = product.description;
        this.cost.text = product.cost + "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this.product);
        float holdTime = Time.time - pointerDownStartTime;

        pointerDownStartTime = 0;

        if (holdTime > MIN_HOLD_TIME)
        {
            removeToggle.SetIsOnWithoutNotify(true);
            optionsMenu.RemoveButtonClick();
        }
        else if (optionsMenu.GetCurrentState().Equals(OptionsMenu.OptionsState.Remove))
        {
            removeToggle.isOn = !removeToggle.isOn;
        }
        else if (optionsMenu.GetCurrentState().Equals(OptionsMenu.OptionsState.Edit))
        {

        }
        else
        {
            OnColapsedButtonClick();
        }
    }

    public void SetRemoveState(bool value)
    {
        if (value != removeOpen)
        {
            if (!colapsed)
                OnColapsedButtonClick();
            StartCoroutine(OnClickHold());
            if (!value)
            {
                removeToggle.SetIsOnWithoutNotify(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownStartTime = Time.time;
    }

    private IEnumerator OnClickHold()
    {
        while (lerping)
            yield return null;

        lerping = true;

        Vector2 sizeDelta = removeView.GetComponent<RectTransform>().sizeDelta;
        Vector2 dataViewOffset = dataView.offsetMin;
        RectTransform removeViewTransform = removeView.GetComponent<RectTransform>();

        float startWidth = sizeDelta.x;
        float startLeftPadding = dataViewOffset.x; //padding.left;

        float newWidth = 200;
        int newLeftPadding = 225;
        if (removeOpen)
        {
            newWidth = 0;
            newLeftPadding = 25;
        }

        float time = 0;

        while(time  < 1)
        {
            float width = Mathf.Lerp(startWidth, newWidth, time);
            int leftPadding = (int)Mathf.Lerp(startLeftPadding, newLeftPadding, time);

            sizeDelta.x = width;
            removeViewTransform.sizeDelta = sizeDelta;

            dataViewOffset.x = leftPadding;
            dataView.offsetMin = dataViewOffset;
            LayoutRebuilder.ForceRebuildLayoutImmediate(dataView);
            time += Time.deltaTime * 10;
            yield return null;
        }
        
        removeView.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, sizeDelta.y);
        dataView.offsetMin = new Vector2(newLeftPadding, dataView.offsetMin.y);

        removeOpen = !removeOpen;

        lerping = false;
    }
}

public class BoolEvent : UnityEvent<bool>
{

}

public class ProductEvent : UnityEvent<Product> { }
