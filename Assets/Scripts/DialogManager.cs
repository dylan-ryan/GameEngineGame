using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    private Dialogue[] originalDialogue;
    public PlayerInteraction playerInteraction;
    public PlayerMovement_2D playerController;

    private bool dialogueActive = false;

    public void StartDialogue(Dialogue[] dialogue)
    {
        if (dialogue == null || dialogue.Length == 0)
        {
            return;
        }
        dialogueActive = true;
        originalDialogue = dialogue;
        dialogueQueue.Clear();
        foreach (Dialogue d in dialogue)
        {
            dialogueQueue.Enqueue(d);
        }
        DisplayNextLine();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (dialogueQueue.Count > 0)
        {
            Dialogue currentDialogue = dialogueQueue.Dequeue();
            speakerNameText.text = currentDialogue.speakerName;
            dialogueText.text = currentDialogue.text;
        }
        
    }


    void Update()
    {
        if (playerInteraction.currentInteractable != null && playerInteraction.currentInteractable.GetComponent<Interactablity>() != null)
        {
            Interactablity interactable = playerInteraction.currentInteractable.GetComponent<Interactablity>();

            if (interactable.interaction == Interactablity.Interaction.Dialog && !dialogueActive && Input.GetKeyDown(KeyCode.E))
            {
                dialogueQueue.Clear();
                StartDialogue(originalDialogue);
                dialogueActive = true;
            }
            else if (dialogueActive && Input.GetKeyDown(KeyCode.E))
            {
                DisplayNextLine();
            }
        }
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        speakerNameText.text = "";
        dialogueText.text = "";
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
