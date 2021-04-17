using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;

    public void ShowMainMenu(bool show)
    {
        MainMenu.SetActive(show);
    }
}
