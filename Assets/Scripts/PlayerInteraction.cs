using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactablity;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInteractable;

    [SerializeField]
    private Interactablity currentInteractableScript;

    private int mushroomsCollected = 0;

    public GameObject mushroomQuest;

    public DialogManager dialogManager;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            switch (currentInteractableScript.interaction)
            {
                case Interaction.Info:
                    currentInteractableScript.InfoText();
                    Debug.Log("Sign");
                    break;
                case Interaction.Log:
                    currentInteractableScript.LogInteraction();
                    Debug.Log("Log");
                    break;
                case Interaction.Mushroom:
                    currentInteractableScript.MushroomInteraction();
                    Debug.Log("Mushroom");
                    break;
                case Interaction.Honey:
                    currentInteractableScript.HoneyInteraction();
                    Debug.Log("Honey");
                    break;
                case Interaction.Water:
                    currentInteractableScript.WaterInteraction();
                    Debug.Log("Water");
                    break;
                case Interaction.Dialog:
                    currentInteractableScript.dialogManager.StartDialogue();
                    break;
            }
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInteractable = other.gameObject;
            currentInteractableScript = currentInteractable.GetComponent<Interactablity>();
            dialogManager = currentInteractable.GetComponent<DialogManager>();
        }
    }
    public void CollectMushroom()
    {
        mushroomsCollected++;
        if (mushroomsCollected >= 2)
        {
            DialogManager questManager = mushroomQuest.GetComponent<DialogManager>();
            if (questManager != null)
            {
                questManager.currentQuestStage = DialogManager.QuestStage.QuestEnd;
            }
            else
            {
                Debug.LogError("QuestManager component not found on the mushroomQuest GameObject.");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInteractable = null;
            currentInteractableScript = null;
        }
    }
}
