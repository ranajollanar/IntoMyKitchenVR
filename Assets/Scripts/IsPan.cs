using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPan : MonoBehaviour
{
    public bool hasFood = false;
    public List<GameObject> food = new List<GameObject>();


    private void Update()
    {
        if (food.Count == 0)
        {
            hasFood = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            hasFood = true;
            food.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            food.Remove(other.gameObject);
        }
    }
}
