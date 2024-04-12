using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Interactablity : MonoBehaviour
{
    [Header("Text Stuff")]
    public string message;
    public TextMeshProUGUI infoText;

    public PlayerInteraction playerInteraction;

    public enum Interaction { Info, Log, Mushroom, Honey, Water, Guard, Dialog, Crown, Inside, Throne }
    [Header("Enum Stuff")]
    public Interaction interaction;
    public DialogManager dialogManager;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerInteraction = GameObject.Find("Player").GetComponent<PlayerInteraction>();
        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
    }

    public void InfoText()
    {
        infoText.text = message;
        StartCoroutine(ShowInfo(message, 2.5f));
        Debug.Log(message);
    }

    IEnumerator ShowInfo(string message, float duration)
    {
        infoText.text = message;
        yield return new WaitForSeconds(duration);
        infoText.text = null;
    }

    public void LogInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectLog();
    }

    public void MushroomInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectMushroom();
    }

    public void HoneyInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectHoney();
    }

    public void WaterInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectWater();
    }

    public void GuardInteraction()
    {
        if(playerInteraction.guardCollected >= 4)
        {
            this.gameObject.SetActive(false);
            playerInteraction.CollectGuard();
        }
    }

    public void CrownInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectCrown();
    }

    public void StartDialogueInteraction()
    {
        if (dialogManager != null)
        {
            playerInteraction.currentInteractable.GetComponent<Collider2D>().enabled = false;
            dialogManager.StartDialogue();
        }
        else
        {
            Debug.LogError("DialogManager reference not set in PlayerInteraction script.");
        }
    }

    public void InsideInteraction()
    {
        SceneManager.LoadScene("Inside");
    }

    public void ThroneInteraction()
    {
        SceneManager.LoadScene("GameOver");
        gameManager.gameState = GameManager.GameState.GameWin;
    }
}

