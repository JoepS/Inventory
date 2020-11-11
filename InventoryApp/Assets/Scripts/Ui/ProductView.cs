using DataManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductView : MonoBehaviour
{
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

    private bool colapsed = true;

    private LayoutElement layoutElement;

    private bool lerping = false;

    private Product product = null;

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

    private void UpdateView()
    {
        this.id.text = product.id + "";
        this.productName.text = product.name;
        this.description.text = product.description;
        this.cost.text = product.cost + "";
    }
}
