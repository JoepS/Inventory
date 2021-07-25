using DataManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecipesListView : TabView
{

    [SerializeField]
    private Transform recipesContent = null;

    [SerializeField]
    private GameObject recipeViewPrefab = null;

    private List<RecipeView> recipeViews = new List<RecipeView>();

    private List<Recipe> recipes = new List<Recipe>();
    
    private void Start()
    {
        recipes = DataManager.instance.GetList<Recipe>().list;
        UpdateProductList();

    }

    private RecipeView CreateProductView(Recipe recipe)
    {
        GameObject recipeViewGameObject = GameObject.Instantiate(recipeViewPrefab, recipesContent);
        recipeViewGameObject.name = recipe.name + "View";

        RecipeView recipeView = recipeViewGameObject.GetComponent<RecipeView>();
        recipeView.SetRecipe(recipe);
        recipeViews.Add(recipeView);
        recipeView.SetOptionsMenu(optionsMenu);
        recipeView.removeToggleSwitch.AddListener(RemoveToggleSwitch);
        recipeView.OnClick.AddListener(OnRecipeViewClick);

        return recipeView;
    }

    private void UpdateProductList()
    {
        foreach (RecipeView pv in recipeViews)
            pv.Hide();
        for (int i = 0; i < recipes.Count; i++)
        {
            RecipeView recipeView = null;
            if (i >= recipeViews.Count)
            {
                recipeView = CreateProductView(recipes[i]);
            }
            else
            {
                recipeView = recipeViews[i];
                recipeView.UpdateRecipe(recipes[i]);
            }
            recipeView.Show();
        }
    }

    private void RemoveToggleSwitch(bool toggled)
    {
        if (!toggled)
        {
            foreach (RecipeView rv in recipeViews)
            {
                if (rv.GetToggleState)
                    return;
            }
            SetRemoveState(false);
        }
    }

    private void OnRecipeViewClick(Recipe recipe)
    {
        if (optionsMenu.currentState.Equals(OptionsMenu.OptionsState.Edit))
        {
            UnityEvent editEndEvent = new UnityEvent();
            editEndEvent.AddListener(OnEndEdit);
            PopupManager.instance.OpenPopup(editIdentifier, editEndEvent, recipe);
        }

    }
    protected override void SetRemoveState(bool value)
    {
        base.SetRemoveState(value);
        foreach (RecipeView rv in recipeViews)
        {
            if (rv.gameObject.activeSelf)
                rv.SetRemoveState(value);
        }
    }

    protected override void SetAddState()
	{
		UnityEvent onEndAdd = new UnityEvent();
		onEndAdd.AddListener(OnEndEdit);
		PopupManager.instance.OpenPopup(addIdentifier, onEndAdd);
	}

	private void OnEndEdit()
    {
        UpdateProductList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }
    protected override void RemoveButtonClick()
    {
        UnityEvent removeConfirmation = new UnityEvent();
        removeConfirmation.AddListener(RemoveConfirmation);
        PopupManager.instance.OpenPopup(confirmationIdentifier, removeConfirmation);
    }

    private void RemoveConfirmation()
    {
        foreach (RecipeView rv in recipeViews)
        {
            if (rv.GetToggleState)
            {
                DataManager.instance.GetList<Recipe>().list.Remove(rv.GetRecipe());
            }
        }

        UpdateProductList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

}
