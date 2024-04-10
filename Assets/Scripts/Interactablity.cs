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
    public Dialogue[] dialogues;

    public enum Interaction { Info, Log, Mushroom, Honey, Water, Dialog }
    [Header("Enum Stuff")]
    public Interaction interaction;
    public DialogManager dialogManager;

    public void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        dialogManager = FindObjectOfType<DialogManager>();
    }

    public void Update()
    {

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
    }

    public void HoneyInteraction()
    {
        this.gameObject.SetActive(false);
    }

    public void WaterInteraction()
    {
        this.gameObject.SetActive(false);
    }

    public void DialogInteraction()
    {
        PlayerInteraction playerInteraction = FindObjectOfType<PlayerInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.StartDialogueInteraction(dialogues);
        }
    }
}
