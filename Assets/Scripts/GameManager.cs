using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private PlayerMovement_2D playerMovement_2D;
    private UIManager uiManager;
    public LevelManager levelManager;

    public GameObject spawn;

    public GameObject character;
    private SpriteRenderer characterArt;

    public enum GameState {MainMenu,Pause,Gameplay,GameWin,GameOver,Options}
    public GameState gameState;
    public void Start()
    {
        character.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        characterArt = character.GetComponent<SpriteRenderer>();
        playerMovement_2D = GetComponent<PlayerMovement_2D>();
        uiManager = GetComponent<UIManager>();
        levelManager = GetComponent<LevelManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        character.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }
    public void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                MainMenuUI();
                break;
            case GameState.Pause:
                PauseUI();
                break;
            case GameState.Gameplay:
                GameplayUI();
                break;
            case GameState.GameWin:
                GameWinUI();
                break;
            case GameState.GameOver:
                GameOverUI();
                break;
            case GameState.Options:
                OptionsUI();
                break;
        }

        if(Input.GetKeyUp(KeyCode.Escape) && gameState == GameState.Gameplay)
        {
            gameState = GameState.Pause;
        }
        else if(Input.GetKeyUp(KeyCode.Escape) && gameState == GameState.Pause)
        {
            gameState = GameState.Gameplay;
        }
    }

    public void MainMenuUI()
    {
        uiManager.ManagerMainMenuUI();
        characterArt.enabled = false;
        character.GetComponent<PlayerMovement_2D>().enabled = false;

    }

    public void PauseUI()
    {     
        uiManager.ManagerPauseUI();
        character.GetComponent<PlayerMovement_2D>().enabled = false;
    }

    public void GameplayUI()
    {
        uiManager.ManagerGameplayUI();
        characterArt.enabled = true;
        character.GetComponent<PlayerMovement_2D>().enabled = true;

    }

    public void GameWinUI()
    {
        uiManager.ManagerGameWinUI();
        characterArt.enabled = false;
        character.GetComponent<PlayerMovement_2D>().enabled = false;
    }

    public void GameOverUI()
    {
        uiManager.ManagerGameOverUI();
        characterArt.enabled = false;
        character.GetComponent<PlayerMovement_2D>().enabled = false;
    }

    public void OptionsUI()
    {
        uiManager.ManagerOptionsUI();
        character.GetComponent<PlayerMovement_2D>().enabled = false;
    }
}
