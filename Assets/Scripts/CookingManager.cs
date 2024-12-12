using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookingManager : MonoBehaviour
{
    public bool panOnStove = false;
    private IsPan panObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            panOnStove = true;
            panObject = other.gameObject.GetComponent<IsPan>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            // Check if panObject is not null before accessing its food list
            if (panObject != null)
            {
                foreach (var item in panObject.food)
                {
                    // Safely check if item has a CookStage component
                    CookStage cookStage = item.GetComponent<CookStage>();
                    if (cookStage != null)
                    {
                        cookStage.isCooking = false;
                    }
                }
            }
            panOnStove = false;
            panObject = null; // Reset panObject after processing
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if 'other' is tagged as 'Pan' and has the XRGrabInteractable component
        if (other.CompareTag("Pan"))
        {
            XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }
            else
            {
                Debug.LogWarning("XRGrabInteractable component not found on the Pan object.");
            }
        }

        // Check if the pan is on the stove and contains food
        if (other.CompareTag("Pan") && panOnStove && panObject != null && panObject.hasFood)
        {
            if (panObject.food != null)
            {
                foreach (GameObject foodItem in panObject.food)
                {
                    if (foodItem != null)
                    {
                        CookStage cookStage = foodItem.GetComponent<CookStage>();
                        if (cookStage != null)
                        {
                            cookStage.StartCooking();
                        }
                    }
                }
            }
        }
    }

}