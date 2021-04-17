using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int recipeLength = 5;

    IngredientType[] recipe;
    List<IngredientType> addedIngredients;
    int correctIngredients;
    int wrongIngredients;

    // Start is called before the first frame update
    void Start()
    {
        addedIngredients = new List<IngredientType>();
        GenerateRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRecipe()
    {
        recipe = new IngredientType[recipeLength];
        for (int i = 0; i < recipeLength; i++)
        {
            int ingredientIndex = Random.Range(0, (int)IngredientType.INGREDIENT_TYPE_COUNT);
            recipe[i] = (IngredientType)ingredientIndex;
            Debug.Log(recipe[i]);
        }
    }

    public void AddIngredient(IngredientType type)
    {
        addedIngredients.Add(type);
        if (type == recipe[addedIngredients.Count - 1] && wrongIngredients == 0)
        {
            correctIngredients++;
        }
        else
        {
            wrongIngredients++;
        }
    }

    public void RemoveNewestIngredient()
    {
        addedIngredients.RemoveAt(addedIngredients.Count - 1);
        if (wrongIngredients != 0)
        {
            wrongIngredients--;
        }
        else
        {
            correctIngredients--;
        }
    }
}
