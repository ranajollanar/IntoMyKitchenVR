using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private XROrigin xrRig;
    [SerializeField] private Canvas xrCanvas;

    private void Start()
    {
        xrRig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        xrRig.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
    }

    public void StartGame()
    {
        xrRig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
        xrRig.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
        xrCanvas.gameObject.SetActive(false);
    }
}
