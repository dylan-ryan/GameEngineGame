using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pause;
    public GameObject gameplay;
    public GameObject gameWin;
    public void ManagerMainMenuUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(true);
        pause.SetActive(false);
        gameplay.SetActive(false);
        gameWin.SetActive(false);
    }

    public void ManagerPauseUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(false);
        pause.SetActive(true);
        gameplay.SetActive(false);
        gameWin.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ManagerGameplayUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameplay.SetActive(true);
        gameWin.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ManagerGameWinUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameplay.SetActive(false);
        gameWin.SetActive(true);
    }

    public void ManagerGameOverUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameplay.SetActive(false);
        gameWin.SetActive(false);
    }

    public void ManagerOptionsUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainMenu.SetActive(false);
        pause.SetActive(false);
        gameplay.SetActive(false);
        gameWin.SetActive(false);
        Time.timeScale = 0f;
    }
}
