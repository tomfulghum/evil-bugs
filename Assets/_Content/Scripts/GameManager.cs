using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject playerCamera;

    [Header("Configuration")]
    [SerializeField] int recipeLength = 5;

    IngredientType[] recipe;
    List<IngredientType> addedIngredients;
    int correctIngredients;
    int wrongIngredients;

    MenuManager menuManager;

    public bool inputDisabled { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        addedIngredients = new List<IngredientType>();
        inputDisabled = true;
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
            int ingredientIndex = Random.Range(0, (int)IngredientType.Cleanser);
            recipe[i] = (IngredientType)ingredientIndex;
            Debug.Log(recipe[i]);
        }
    }

    public void AddIngredient(IngredientType type)
    {
        if (type == IngredientType.Cleanser)
        {
            RemoveNewestIngredient();
            return;
        }

        addedIngredients.Add(type);
         if (addedIngredients.Count <= recipe.Length && type == recipe[addedIngredients.Count - 1] && wrongIngredients == 0)
        {
            correctIngredients++;
            if (correctIngredients == recipe.Length)
            {
                EndGame(true);
            }
        }
        else
        {
            wrongIngredients++;
        }
    }

    public void RemoveNewestIngredient()
    {
        if (addedIngredients.Count == 0)
            return;

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

    public void StartGame()
    {
        menuManager.ShowMainMenu(false);
        playerCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GenerateRecipe();
        StartCoroutine(StartGameCoroutine());
    }

    public void EndGame(bool win)
    {

    }

    public void OnReset()
    {
        correctIngredients = 0;
        wrongIngredients = 0;
        addedIngredients.Clear();
        GenerateRecipe();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        inputDisabled = false;
    }
}
