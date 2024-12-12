using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    private bool panOnStove = false;
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
            panOnStove = false;
            panObject = null;
            foreach (var item in panObject.food)
            {
                item.GetComponent<CookStage>().isCooking = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pan") && panOnStove && panObject != null && panObject.hasFood)
        {
            // Trigger cooking on each food item in the pan
            foreach (GameObject foodItem in panObject.food)
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