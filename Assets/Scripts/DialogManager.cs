using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public enum QuestStage
    {
        QuestStart,
        QuestMiddle,
        QuestEndDialogue,
        QuestEnd
    }

    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;

    public QuestStage currentQuestStage;
    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    public bool dialogueActive = false;

    public Dialogue[] questStartDialogues;
    public Dialogue[] questMiddleDialogues;
    public Dialogue[] questEndDialogues;

    private PlayerInteraction playerInteraction;

    private void Update()
    {
        if (dialogueActive == true)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement_2D>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        }
        if (dialogueActive == false)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement_2D>().enabled = true;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void Start()
    {
        playerInteraction = GameObject.Find("Player").GetComponent<PlayerInteraction>();
        currentQuestStage = QuestStage.QuestStart;
    }

    public void StartDialogue()
    {      
        Dialogue[] dialogues = GetDialoguesForStage(currentQuestStage);

        if (dialogues.Length == 0)
        {
            Debug.LogError("No dialogues provided for the current stage.");
            return;
        }

        if (dialogueActive)
        {
            Debug.LogWarning("Dialogue already active.");
            return;
        }

        dialogueActive = true;

        StartCoroutine(WaitForPlayerInput(dialogues));
    }

    private Dialogue[] GetDialoguesForStage(QuestStage questStage)
    {
        switch (questStage)
        {
            case QuestStage.QuestStart:
                return questStartDialogues;
            case QuestStage.QuestMiddle:
                return questMiddleDialogues;
            case QuestStage.QuestEndDialogue:
                return questEndDialogues;
            case QuestStage.QuestEnd:
                return questEndDialogues;
            default:
                return new Dialogue[0];
        }
    }

    private IEnumerator WaitForPlayerInput(Dialogue[] dialogues)
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueQueue.Enqueue(dialogues[i]);
        }

        while (dialogueQueue.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DisplayNextLine();
            }
            yield return null;
        }
        EndDialogue();
    }
            
    private void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogue currentDialogue = dialogueQueue.Dequeue();
        speakerNameText.text = currentDialogue.speakerName;
        dialogueText.text = currentDialogue.text;
    }

    public void EndDialogue()
    {
        if (currentQuestStage == QuestStage.QuestStart)
        {
            currentQuestStage = QuestStage.QuestMiddle;
        }
        else if (currentQuestStage == QuestStage.QuestEndDialogue && dialogueQueue.Count == 0)
        {
            currentQuestStage = QuestStage.QuestEnd;
            playerInteraction.guardCollected++;
        }
        else if (currentQuestStage == QuestStage.QuestEnd)
        {
            Debug.Log("Quest Done");
        }
        else
        {
            Debug.LogWarning("Unknown quest stage or already at the end.");
        }

        dialogueActive = false;
        speakerNameText.text = "";
        dialogueText.text = "";
        Debug.Log("Dialogue ended");
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

}
