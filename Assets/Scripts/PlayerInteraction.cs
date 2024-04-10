using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactablity;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInteractable;
    public Interactablity currentInteractableScript;
    private bool inDialogue = false;

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
                    StartDialogueInteraction(currentInteractableScript.dialogues);
                    break;
            }
        }
    }
    public void StartDialogueInteraction(Dialogue[] dialogues)
    {
        if (!inDialogue)
        {
            inDialogue = true;
            FindObjectOfType<DialogManager>().StartDialogue(dialogues);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInteractable = other.gameObject;
            currentInteractableScript = currentInteractable.GetComponent<Interactablity>();
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
