using DataManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductAmountView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    private const float MIN_HOLD_TIME = 1;

    [SerializeField]
    private TMP_Text productName = null;

    [SerializeField]
    private TMP_Text amountValue = null;

    [SerializeField]
    private TMP_Text amountType = null;

    [SerializeField]
    private GameObject perishPanel = null;

    [SerializeField]
    private TMP_Text perishDate = null;

       
    [SerializeField]
    private Toggle removeToggle = null;

    [SerializeField]
    private GameObject removeView = null;

    [SerializeField]
    private RectTransform dataView = null;

    public ProductAmountEvent OnClick = new ProductAmountEvent();

    private OptionsMenu optionsMenu;

    private ProductAmount productAmount;

    private bool removeOpen = false;

    private bool lerping = false;

    private float pointerDownStartTime = 0;

    public bool GetToggleState
    {
        get
        {
            return removeToggle.isOn;
        }
    }

    public BoolEvent removeToggleSwitch = new BoolEvent();

    private void Awake()
    {

        removeToggle.onValueChanged.AddListener(delegate { removeToggleSwitch.Invoke(removeToggle.isOn); });
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this.productAmount);
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
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownStartTime = Time.time;
    }

    public void Setup(Product product, ProductAmount amount)
    {
        productAmount = amount;
        this.productName.text = product.name;
        amountValue.text = amount.amount.value.ToString();
        amountType.text = amount.amount.type.ToString();

        
        perishPanel.SetActive(product.perishable);
        perishDate.text = amount.perishDate;
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

    public ProductAmount GetProductAmount()
    {
        return productAmount;
    }
    public void SetRemoveState(bool value)
    {
        if (value != removeOpen)
        {
            StartCoroutine(OnClickHold());
            if (!value)
            {
                removeToggle.SetIsOnWithoutNotify(false);
            }
        }
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

        while (time < 1)
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

public class ProductAmountEvent : UnityEvent<ProductAmount> { }
