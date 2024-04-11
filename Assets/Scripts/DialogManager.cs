using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject thisGameObject;
    // Remove public QuestManager questManager;

    public Dialogue[] questStartDialogues;
    public Dialogue[] questMiddleDialogues;
    public Dialogue[] questEndDialogues;

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    public PlayerInteraction playerInteraction;
    public PlayerMovement_2D playerController;
    private bool dialogueActive = false;

    public void Start()
    {
    }

    public void StartDialogue(QuestManager.QuestStage questStage, Dialogue[] dialogues)
    {
        QuestManager questManager = thisGameObject.GetComponent<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene.");
            return;
        }

        dialogueActive = true;
        questManager.SetCurrentQuestStage(questStage);

        StartCoroutine(WaitForPlayerInput(dialogues));
    }

    private IEnumerator WaitForPlayerInput(Dialogue[] dialogues)
    {
        foreach (Dialogue d in dialogues)
        {
            dialogueQueue.Enqueue(d);
        }

        while (dialogueQueue.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextLine();
            }
            yield return null;
        }
        EndDialogue(thisGameObject.GetComponent<QuestManager>());
    }




    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue(gameObject.GetComponent<QuestManager>());
            return;
        }

        Dialogue currentDialogue = dialogueQueue.Dequeue();
        speakerNameText.text = currentDialogue.speakerName;
        dialogueText.text = currentDialogue.text;
    }

    public void HandleDialogueInteraction() { 

            if (!dialogueActive && playerInteraction.currentInteractable != null &&
                playerInteraction.currentInteractable.GetComponent<Interactablity>() != null)
            {
                Interactablity interactable = playerInteraction.currentInteractable.GetComponent<Interactablity>();

                if (interactable.interaction == Interactablity.Interaction.Dialog)
                {
                    QuestManager questManager = thisGameObject.GetComponent<QuestManager>();
                    if (questManager != null)
                    {
                        QuestManager.QuestStage currentQuestStage = questManager.GetCurrentQuestStage();
                        switch (currentQuestStage)
                        {
                            case QuestManager.QuestStage.QuestStart:
                                StartDialogue(QuestManager.QuestStage.QuestStart, questStartDialogues);
                                break;
                            case QuestManager.QuestStage.QuestMiddle:
                                StartDialogue(QuestManager.QuestStage.QuestMiddle, questMiddleDialogues);
                                break;
                            case QuestManager.QuestStage.QuestEnd:
                                StartDialogue(QuestManager.QuestStage.QuestEnd, questEndDialogues);
                                break;
                            default:
                                Debug.LogError("Unknown quest stage!");
                                break;
                        }
                    }
                    else
                    {
                        Debug.LogError("QuestManager not found in the scene.");
                    }
                }
            }
            else if (dialogueActive)
            {
                DisplayNextLine();
            }
        
    }


    private void EndDialogue(QuestManager questManager)
    {
        dialogueActive = false;
        speakerNameText.text = "";
        dialogueText.text = "";
        Debug.Log("Dialogue ended");

        if (questManager.GetCurrentQuestStage() == QuestManager.QuestStage.QuestStart)
        {
            questManager.SetCurrentQuestStage(QuestManager.QuestStage.QuestMiddle);
        }

        if (playerController != null)
        {
            playerController.enabled = true;
            Debug.Log("Player controller enabled");
        }
    }
}
