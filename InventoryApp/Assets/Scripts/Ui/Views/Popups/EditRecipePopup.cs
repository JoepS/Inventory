using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditRecipePopup : PopupView
{
    [SerializeField]
    private Button addIngredientButton = null;

    [SerializeField]
    private Button saveButton = null;

    [SerializeField]
    private GameObject ingredientView = null;

    [SerializeField]
    private GameObject addIngredientView = null;

    [SerializeField]
    private ValueDropdown productDropdown = null;

    [SerializeField]
    private Transform ingredientListParent = null;

    [SerializeField]
    private Identifier recipeName = null;

    [SerializeField]
    private Identifier description = null;

    [SerializeField]
    private Identifier preperationTime = null;

    [SerializeField]
    private Identifier amountValue = null;

    [SerializeField]
    private Identifier amountType = null;

    private Recipe currentEdit;

    private List<ProductAmount> ingredients = new List<ProductAmount>();
    public List<IngredientView> ingredientViews = new List<IngredientView>();

    protected override void Awake()
    {
        base.Awake();
        addIngredientButton.onClick.AddListener(OnAddIngredientClick);
        saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    public override void Open(object[] optionals)
    {
        base.Open(optionals);

        if (optionals.Length > 1)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
            if (optionals[1].GetType().Equals(typeof(Recipe)))
            {
                currentEdit = (Recipe)optionals[1];

                SetupProductEdit(currentEdit);
            }
        }
        else
        {
            Close();
        }

    }
    private void Setup()
    {
        List<Product> products = DataManager.instance.GetList<Product>().list;
        List<ValueDropdown.OptionData> options = new List<ValueDropdown.OptionData>();
        foreach (Product product in products)
        {
            if (ingredients.Where(x => x.ProductId == product.id).Count() == 0)
            {
                options.Add(new ValueDropdown.OptionData()
                {
                    text = product.name
                });
            }
        }

        addIngredientView.SetActive(options.Count != 0);
        productDropdown.options = options;
        productDropdown.value = 0;
    }

    private void SetupProductEdit(Recipe recipe)
    {
        GetFromIdentifier(recipeName).SetValue(recipe.name);
        GetFromIdentifier(description).SetValue(recipe.description);
        GetFromIdentifier(preperationTime).SetValue(recipe.preperationTime);

        ProductAmount[] temp = new ProductAmount[recipe.ingredients.Count];
        recipe.ingredients.CopyTo(temp);
        ingredients = temp.ToList();


        foreach (IngredientView iv in ingredientViews)
        {
            if (iv != null)
                iv.Hide();

        }

        for (int i = 0; i < ingredients.Count; i++)
        {
            ProductAmount ingredient = ingredients[i];

            IngredientView ingredientView = GetIngredientView(ingredient);
            ingredientView.View(ingredient, this);
        }

        Setup();
    }
    
    private IngredientView CreateIngredientView(ProductAmount ingredient)
    {
        Debug.LogWarning("Create new ingredient view");
        GameObject ingredientViewObject = GameObject.Instantiate(this.ingredientView, ingredientListParent);
        ingredientViewObject.name = "IngredientView " + ingredient.ProductId;
        IngredientView ingredientView = ingredientViewObject.GetComponent<IngredientView>();
        ingredientViews.Add(ingredientView);
        ingredientView.View(ingredient);
        return ingredientView;
    }

    private void OnAddIngredientClick()
    {

        Product product = DataManager.instance.GetList<Product>().list.Where(x => x.name.Equals(productDropdown.options[productDropdown.value].text)).First();

        ProductAmount productAmount = new ProductAmount()
        {
            ProductId = product.id,
            amount = new Amount()
            {
                value = float.Parse(GetFromIdentifier(amountValue).GetValue().ToString()),
                type = (Amount.Type)GetFromIdentifier(amountType).GetValue()
            }
        };

        ingredients.Add(productAmount);

        IngredientView ingredientView = GetIngredientView(productAmount);
        ingredientView.View(productAmount);

        Setup();

    }

    public IngredientView GetIngredientView(ProductAmount ingredient) 
    {
        for (int i = 0; i < ingredientViews.Count; i++)
        {
            if (ingredientViews[i].IsHidden())
            {
                return ingredientViews[i];
            }
        }

        return CreateIngredientView(ingredient);


    }

    private void OnSaveButtonClick()
    {
        Debug.Log(GetFromIdentifier(recipeName));
        currentEdit.name = GetFromIdentifier(recipeName).GetValue().ToString();
        currentEdit.description = GetFromIdentifier(this.description).GetValue().ToString();
        currentEdit.preperationTime = int.Parse(GetFromIdentifier(this.preperationTime).GetValue().ToString());

        currentEdit.ingredients = ingredients;
        

        DataManager.instance.EditItem(currentEdit);

        Close();
    }
    public override void Remove<T>(T ingredient)
    {
        Debug.Log(typeof(T));
        if (typeof(T).Equals(typeof(ProductAmount)))
        {
            object temp = (object)ingredient;
            ingredients.Remove((ProductAmount)temp);
            ingredientViews.Where(x => x.productAmount.Equals((ProductAmount)temp)).First().Hide();
            Setup();
        }
    }
}
