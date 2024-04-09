using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactablity;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInteractable;

    public Interactablity currentInteractableScript;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            if (currentInteractable.GetComponent<Interactablity>().interaction == Interaction.Info)
            {
                currentInteractableScript.InfoText();
            }
            if (currentInteractable.GetComponent<Interactablity>().interaction == Interaction.Log)
            {
                currentInteractableScript.LogInteraction();
                Debug.Log("Coin");
            }
            else if (currentInteractable.GetComponent<Interactablity>().interaction == Interaction.Mushroom)
            {
                currentInteractableScript.MushroomInteraction();
                Debug.Log("Gem");
            }
        }
    }

    public void DoInteraction()
    {

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
