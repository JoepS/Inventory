using DataManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ProductView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
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
    private VerticalLayoutGroup layoutGroup = null;


    private bool colapsed = true;

    private bool removeOpen = false;

    private LayoutElement layoutElement;

    private bool lerping = false;

    private Product product = null;

    float pointerDownStartTime = 0;

    private void Awake()
    {
        layoutElement = this.GetComponent<LayoutElement>();
        colapsedButton.onClick.AddListener(OnColapsedButtonClick);
    }

    private void OnColapsedButtonClick()
    {
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
        if(Time.time - pointerDownStartTime < MIN_HOLD_TIME && !removeOpen)
            OnColapsedButtonClick();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float holdTime = Time.time - pointerDownStartTime;

        if(holdTime > MIN_HOLD_TIME)
        {
            Debug.Log("HoldTime: " + holdTime);
            if (!colapsed)
                OnColapsedButtonClick();
            StartCoroutine(OnClickHold());

        }
    }

    private IEnumerator OnClickHold()
    {
        while (lerping)
            yield return null;

        lerping = true;

        Vector2 sizeDelta = removeView.GetComponent<RectTransform>().sizeDelta;
        RectTransform layoutGroupTransform = layoutGroup.GetComponent<RectTransform>();
        RectTransform removeViewTransform = removeView.GetComponent<RectTransform>();

        float startWidth = sizeDelta.x;
        int startLeftPadding = layoutGroup.padding.left;

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
            layoutGroup.padding.left = leftPadding;
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroupTransform);
            time += Time.deltaTime * 10;
            yield return null;
        }
        
        removeView.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, sizeDelta.y);
        layoutGroup.padding.left = newLeftPadding;

        removeOpen = !removeOpen;

        lerping = false;
    }
}
