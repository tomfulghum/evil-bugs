using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] TextMeshProUGUI[] ingredientEntries;

    [Header("Configuration")]
    [SerializeField] int recipeLength = 5;
    [SerializeField] float badExplosionStrength = 10f;

    GameState state;
    IngredientType[] recipe;
    List<IngredientType> addedIngredients;

    MenuManager menuManager;

    public bool inputDisabled { get; private set; }
    public bool cameraDisabled { get; private set; }
    public bool paused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        addedIngredients = new List<IngredientType>();
        state = GameState.Menu;
        inputDisabled = true;
        cameraDisabled = true;
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
            ingredientEntries[i].text = recipe[i].ToString();
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
            playerRb.AddTorque(explosionDirection * badExplosionStrength);
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
            state = GameState.End;
            menuManager.ShowWinMenu(true);
            inputDisabled = true;
            cameraDisabled = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnReset()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
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
            cameraDisabled = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            menuManager.ShowPauseMenu(true);
        }
        else
        {
            state = GameState.Running;
            cameraDisabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            menuManager.ShowPauseMenu(false);
        }
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        inputDisabled = false;
        cameraDisabled = false;
    }

    IEnumerator BadIngredientCoroutine()
    {
        inputDisabled = true;
        yield return new WaitForSeconds(5f);
        inputDisabled = false;
    }
}
