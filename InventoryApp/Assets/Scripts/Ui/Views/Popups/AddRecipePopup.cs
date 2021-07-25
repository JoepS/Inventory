using DataManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class AddRecipePopup : PopupView
{
    [SerializeField]
    private Identifier recipeName = null;

    [SerializeField]
    private Identifier description = null;

    [SerializeField]
    private Identifier preperationTime = null;

    [SerializeField]
    private Button addIngredientButton = null;

    [SerializeField]
    private Transform ingredientListParent = null;

    [SerializeField]
    private GameObject ingredientView = null;

    [SerializeField]
    private GameObject addIngredientView = null;

    [SerializeField]
    private ValueDropdown productDropdown = null;

    [SerializeField]
    private Identifier amountValue = null;

    [SerializeField]
    private Identifier amountType = null;

    [SerializeField]
    private Button saveButton = null;

    private List<ProductAmount> ingredients = new List<ProductAmount>();

    protected override void Awake()
    {
        base.Awake();

        addIngredientButton.onClick.AddListener(OnAddIngredientClick);
        saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    public override void Open(object[] optionals)
    {
        base.Open(optionals);

        if (optionals.Length > 0)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
        }

        Setup();
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

        GameObject ingredientView = GameObject.Instantiate(this.ingredientView, ingredientListParent);
        ingredientView.GetComponent<IngredientView>().View(productAmount, this);

        Setup();

    }

    private void OnSaveButtonClick()
    {
        Debug.Log(GetFromIdentifier(recipeName));
        string name = GetFromIdentifier(recipeName).GetValue().ToString();
        string description = GetFromIdentifier(this.description).GetValue().ToString();
        int preperationTime = int.Parse(GetFromIdentifier(this.preperationTime).GetValue().ToString());

        Recipe recipe = new Recipe()
        {
            name = name,
            description = description,
            preperationTime = preperationTime,
            ingredients = ingredients
        };

        DataManager.instance.AddItem(recipe);

        Close();
    }

    public override void Remove<T>(T ingredient)
    {
        if (typeof(T).Equals(typeof(ProductAmount)))
        {
            object temp = (object)ingredient;
            ingredients.Remove((ProductAmount)temp);
            Setup();
        }
    }
}
