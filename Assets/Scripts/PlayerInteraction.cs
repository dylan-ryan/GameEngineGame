using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Interactablity;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Currently Interacting With")]
    public GameObject currentInteractable;

    [SerializeField]
    private Interactablity currentInteractableScript;

    [Header("Quest Progress")]
    public int mushroomsCollected = 0;
    public int logCollected = 0;
    public int honeyCollected = 0;
    public int waterCollected = 0;
    public int guardCollected = 0;
    public int crownCollected = 0;

    [Header("Quest Objects")]
    [SerializeField]
    private GameObject mushroomQuest;
    [SerializeField]
    private GameObject logQuest;
    [SerializeField]
    private GameObject honeyQuest;
    [SerializeField]
    private GameObject waterQuest;
    [SerializeField]
    private GameObject guardQuest;
    [SerializeField]
    private GameObject kingQuest;

    [Header("Current DialogManager")]
    public DialogManager dialogManager;

    [Header("Inventory Icons")]
    public GameObject waterImage;
    public GameObject coinImage;
    public GameObject shroomImage;
    public GameObject honeyImage;
    public GameObject logImage;

    private void Start()
    {
    }

    void Update()
    {
        CollectGuard();
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
                case Interaction.Guard:
                    currentInteractableScript.GuardInteraction();
                    Debug.Log("Guard");
                    break;
                case Interaction.Dialog:
                    currentInteractableScript.StartDialogueInteraction();
                    break;
                case Interaction.Crown:
                    currentInteractableScript.CrownInteraction();
                    break;
                case Interaction.Inside:
                    currentInteractableScript.InsideInteraction();
                    break;
                case Interaction.Throne:
                    currentInteractableScript.ThroneInteraction();
                    break;
            }
        }
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            mushroomQuest = GameObject.Find("MushroomQuest");
            logQuest = GameObject.Find("LogQuest");
            honeyQuest = GameObject.Find("HoneyQuest");
            waterQuest = GameObject.Find("WaterQuest");
            guardQuest = GameObject.Find("GuardQuest");
            kingQuest = GameObject.Find("KingQuest");
        }
        if (mushroomQuest.GetComponent<DialogManager>().currentQuestStage == DialogManager.QuestStage.QuestEnd)
        {
            shroomImage.SetActive(false);
        }
        if(waterQuest.GetComponent<DialogManager>().currentQuestStage == DialogManager.QuestStage.QuestEnd)
        {
            waterImage.SetActive(false);
        }
        if(logQuest.GetComponent<DialogManager>().currentQuestStage == DialogManager.QuestStage.QuestEnd)
        {
            logImage.SetActive(false);
        }
        if(honeyQuest.GetComponent<DialogManager>().currentQuestStage == DialogManager.QuestStage.QuestEnd)
        {
            honeyImage.SetActive(false);
        }
        if(kingQuest.GetComponent<DialogManager>().currentQuestStage == DialogManager.QuestStage.QuestEnd)
        {
            coinImage.SetActive(false);
        }

    }

    public void CollectGuard()
    {
        if (guardQuest != null && guardCollected >= 4)
        {
            DialogManager dialogManager = guardQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
                var boxCollider = guardQuest.GetComponentInChildren<BoxCollider2D>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
                else
                {
                    Debug.LogError("BoxCollider2D component not found on the guardQuest GameObject.");
                }
            }
            else
            {
                Debug.LogError("QuestManager component not found on the guardQuest GameObject.");
            }
        }
    }


    public void CollectMushroom()
    {
        mushroomsCollected++;
        if (mushroomsCollected >= 2 && mushroomQuest != null)
        {   
            shroomImage.SetActive(true);
            DialogManager dialogManager = mushroomQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
            }
            else
            {
                Debug.LogError("DialogManager component not found on the mushroomQuest GameObject.");
            }
        }
    }

    public void CollectLog()
    {
        logCollected++;
        if (logCollected >= 3 && logQuest != null)
        {
            logImage.SetActive(true);
            DialogManager dialogManager = logQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
            }
            else
            {
                Debug.LogError("DialogManager component not found on the logQuest GameObject.");
            }
        }
    }

    public void CollectHoney()
    {
        honeyCollected++;
        if (honeyCollected >= 6 && honeyQuest != null)
        {
            honeyImage.SetActive(true);
            DialogManager dialogManager = honeyQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
            }
            else
            {
                Debug.LogError("DialogManager component not found on the honeyQuest GameObject.");
            }
        }
    }

    public void CollectCrown()
    {
        crownCollected++;
        if (crownCollected >= 1 && kingQuest != null)
        {
            coinImage.SetActive(true);
            DialogManager dialogManager = kingQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
                BoxCollider2D boxCollider = kingQuest.GetComponentInChildren<BoxCollider2D>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                    crownCollected = 0;
                }
                else
                {
                    Debug.LogError("BoxCollider2D component not found on the kingQuest GameObject.");
                }
            }
            else
            {
                Debug.LogError("DialogManager component not found on the kingQuest GameObject.");
            }
        }
    }

    public void CollectWater()
    {
        waterCollected++;
        if (waterCollected >= 1 && waterQuest != null)
        {
            waterImage.SetActive(true);
            DialogManager dialogManager = waterQuest.GetComponent<DialogManager>();
            if (dialogManager != null)
            {
                dialogManager.currentQuestStage = DialogManager.QuestStage.QuestEndDialogue;
            }
            else
            {
                Debug.LogError("DialogManager component not found on the waterQuest GameObject.");
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

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInteractable = null;
            currentInteractableScript = null;
        }
    }
}
