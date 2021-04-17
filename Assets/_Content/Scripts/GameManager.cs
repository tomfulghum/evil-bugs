using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Menu,
        Running,
        Paused,
        End
    }

    [Header("References")]
    [SerializeField] GameObject playerCamera;

    [Header("Configuration")]
    [SerializeField] int recipeLength = 5;
    [SerializeField] float badExplosionStrength = 10f;

    GameState state;
    IngredientType[] recipe;
    List<IngredientType> addedIngredients;

    MenuManager menuManager;

    public bool inputDisabled { get; private set; }
    public bool paused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        addedIngredients = new List<IngredientType>();
        state = GameState.Menu;
        inputDisabled = true;
        paused = false;
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
        if (type != recipe[addedIngredients.Count])
        {
            Debug.Log("BAD");
            var playerRb = FindObjectOfType<FlightMovement>().GetComponent<Rigidbody>();
            var explosionDirection = Random.onUnitSphere;
            explosionDirection = new Vector3(explosionDirection.x, Mathf.Abs(explosionDirection.y), explosionDirection.z);
            playerRb.AddForce(explosionDirection * badExplosionStrength, ForceMode.Impulse);
            StartCoroutine(BadIngredientCoroutine());
            return;
        }

        addedIngredients.Add(type);
        if (addedIngredients.Count == recipe.Length)
        {
            EndGame(true);
        }
    }

    public void StartGame()
    {
        state = GameState.Running;
        menuManager.ShowMainMenu(false);
        playerCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        GenerateRecipe();
        StartCoroutine(StartGameCoroutine());
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            // yay
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPause()
    {
        if (state == GameState.Menu || state == GameState.End)
            return;

        paused = !paused;
        if (paused)
        {
            state = GameState.Paused;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            menuManager.ShowPauseMenu(true);
        }
        else
        {
            state = GameState.Running;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            menuManager.ShowPauseMenu(false);
        }
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        inputDisabled = false;
    }

    IEnumerator BadIngredientCoroutine()
    {
        inputDisabled = true;
        yield return new WaitForSeconds(5f);
        inputDisabled = false;
    }
}
