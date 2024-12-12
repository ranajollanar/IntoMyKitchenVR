using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPan : MonoBehaviour
{
    public bool hasFood = false;
    public List<GameObject> food = new List<GameObject>();

    private void Update()
    {
        // Update the hasFood flag based on the food list count
        hasFood = food.Count > 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Food") && !food.Contains(other.gameObject))
        {
            // Add the food object only if it's not already in the list
            food.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            // Remove the food object when it exits the collider
            food.Remove(other.gameObject);
        }
    }
}