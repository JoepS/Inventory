using DataManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    private const float MIN_HOLD_TIME = 1;

    [SerializeField]
    private TMP_Text recipeName = null;

    [SerializeField]
    private TMP_Text preperationTime = null;

    [SerializeField]
    private TMP_Text description = null;

    [SerializeField]
    private Transform ingredientsListParent = null;

    [SerializeField]
    private GameObject ingredientView = null;

    [SerializeField]
    private Button colapsedButton = null;

    [SerializeField]
    private VerticalLayoutGroup dataViewLayout = null;

    [SerializeField]
    private GameObject removeView = null;

    [SerializeField]
    private RectTransform dataView = null;

    [SerializeField]
    private RectTransform colapsedTransform = null;

    [SerializeField]
    private Toggle removeToggle = null;

    public BoolEvent removeToggleSwitch = new BoolEvent();

    public RecipeEvent OnClick = new RecipeEvent();

    private Recipe recipe;

    private LayoutElement layoutElement;

    bool lerping = false;

    bool isColapsed = true;

    private float pointerDownStartTime = 0;

    private OptionsMenu optionsMenu;

    private List<IngredientView> ingredientViews = new List<IngredientView>();

    public bool removeOpen = false;
    public bool GetToggleState
    {
        get
        {
            return removeToggle.isOn;
        }
    }


    private void Awake()
    {
        layoutElement = this.GetComponent<LayoutElement>();
        colapsedButton.onClick.AddListener(OnColapsedButtonClick);
        removeToggle.onValueChanged.AddListener(delegate { removeToggleSwitch.Invoke(removeToggle.isOn); });
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public Recipe GetRecipe()
    {
        return this.recipe;
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        UpdateView();
    }
    internal void UpdateRecipe(Recipe recipe)
    {
        SetRecipe(recipe);
    }

    private void UpdateView()
    {
        recipeName.text = recipe.name;
        preperationTime.text = recipe.preperationTime + " min";
        description.text = recipe.description;

        foreach (IngredientView iv in ingredientViews)
            iv.Hide();

        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            ProductAmount ingredient = recipe.ingredients[i];

            IngredientView ingredientView;
            if (i >= ingredientViews.Count)
            {
                ingredientView = CreateIngredientView(ingredient);
            }
            else
            {
                ingredientView = ingredientViews[i];
            }
            ingredientView.View(ingredient);
            ingredientView.SetRemovable(false);
        }
    }

    private IngredientView CreateIngredientView(ProductAmount ingredient)
    {
        GameObject ingredientViewObject = GameObject.Instantiate(this.ingredientView, ingredientsListParent);
        IngredientView ingredientView = ingredientViewObject.GetComponent<IngredientView>();
        ingredientViews.Add(ingredientView);
        return ingredientView;
    }

    public void SetOptionsMenu(OptionsMenu optionsMenu)
    {
        this.optionsMenu = optionsMenu;
    }
    private void OnColapsedButtonClick()
    {
        if (lerping)
            return;

        float newSize = 200;
        if (isColapsed)
        {
            newSize = colapsedTransform.sizeDelta.y + dataViewLayout.spacing + this.GetComponent<RectTransform>().sizeDelta.y;
            isColapsed = false;
        }
        else
        {
            isColapsed = true;
        }
        StartCoroutine(LerpToSize(newSize));
    }
    private IEnumerator LerpToSize(float endSize)
    {
        lerping = true;
        float startSize = layoutElement.minHeight;
        float time = 0;
        float speed = 4;
        while (layoutElement.minHeight != endSize)
        {
            time += speed * Time.deltaTime;
            layoutElement.minHeight = Mathf.Lerp(startSize, endSize, time);
            yield return null;
        }
        layoutElement.minHeight = endSize;
        lerping = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this.recipe);
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
            OnColapsedButtonClick();
        }
        else
        {
            OnColapsedButtonClick();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownStartTime = Time.time;
    }
    public void SetRemoveState(bool value)
    {
        if (value != removeOpen)
        {
            if (!isColapsed)
                OnColapsedButtonClick();
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

public class RecipeEvent : UnityEvent<Recipe> { }
