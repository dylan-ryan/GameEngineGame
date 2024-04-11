using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactablity : MonoBehaviour
{
    [Header("Text Stuff")]
    public string message;
    public TextMeshProUGUI infoText;
    //public Dialogue[] dialogues;

    private PlayerInteraction playerInteraction;

    public enum Interaction { Info, Log, Mushroom, Honey, Water, Dialog }
    [Header("Enum Stuff")]
    public Interaction interaction;
    public DialogManager dialogManager;

    public void Start()
    {
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
    }

    public void MushroomInteraction()
    {
        this.gameObject.SetActive(false);
        playerInteraction.CollectMushroom();
    }

    public void HoneyInteraction()
    {
        this.gameObject.SetActive(false);
    }

    public void WaterInteraction()
    {
        this.gameObject.SetActive(false);
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
}

