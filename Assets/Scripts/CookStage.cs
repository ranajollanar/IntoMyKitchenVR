using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class CookStage : MonoBehaviour
{
    public bool soundPlayed = false;

    [SerializeField] private Material cookedMaterial;
    [SerializeField] private Material mediumCookedMaterial;
    [SerializeField] private float overallCookingTime = 9f;
    [SerializeField] private AudioSource cookingAudio;
    [SerializeField] private AudioSource timeUpAudio;
    [SerializeField] private Canvas timerCanvas;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private ParticleSystem smokeParticles;

    public bool isCooked = false; // Flag to track if it's already cooked
    public bool isCooking = false;

    public void StartCooking()
    {
        // Prevent re-cooking if already cooked
        if (!isCooking && !isCooked)
        {
            smokeParticles.Play();
            cookingAudio.Play();
            StartCoroutine(Cooking());
        }
    }
    

    private IEnumerator Cooking()
    {
        transform.GetComponent<XRGrabInteractable>().enabled = false;
        isCooking = true;
        timerCanvas.gameObject.SetActive(true);

        float remainingTime = overallCookingTime;
        float stage1Time = overallCookingTime / 3f;
        float stage2Time = 2 * stage1Time;

        MeshRenderer renderer = GetComponent<MeshRenderer>();

        // Countdown timer with real-time updates
        while (remainingTime > 0)
        {
            timerText.text = FormatTime(remainingTime);

            // Change to medium cooked material during stage 2
            if (remainingTime <= stage2Time && remainingTime > stage1Time && renderer != null && mediumCookedMaterial != null)
            {
                renderer.material = mediumCookedMaterial;
            }

            // Change to cooked material during stage 3
            if (remainingTime <= stage1Time && renderer != null && cookedMaterial != null)
            {
                renderer.material = cookedMaterial;
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "00:00:00";

        // Play time-up audio when the timer ends
        if (timeUpAudio != null)
        {
            timeUpAudio.Play();
        }

        
        isCooking = false;
        transform.GetComponent<XRGrabInteractable>().enabled = false;
        isCooked = true; 
        //timerCanvas.gameObject.SetActive(false);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:00}:{seconds:00}:{milliseconds / 10:00}";
    }
}
