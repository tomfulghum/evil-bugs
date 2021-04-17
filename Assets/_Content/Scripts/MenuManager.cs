using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;

    public void ShowMainMenu(bool show)
    {
        mainMenu.SetActive(show);
    }

    public void ShowPauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
    }

    public void ShowWinMenu(bool show)
    {
        winMenu.SetActive(show);
    }
}
