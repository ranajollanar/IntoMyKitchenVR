using System;
using System.Collections;
using UnityEngine;
using TMPro;

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

    public bool isCooking = false;

    public void StartCooking()
    {
        if (!isCooking)
        {
            StartCoroutine(Cooking());
        }
    }

    private void Update()
    {
        if (cookingAudio != null)
        {
            if (isCooking && !cookingAudio.isPlaying)
            {
                cookingAudio.Play();
            }
            else if (!isCooking && cookingAudio.isPlaying)
            {
                cookingAudio.Stop();
            }
        }
    }

    private IEnumerator Cooking()
    {
        isCooking = true;
        timerCanvas.enabled = true;

        float remainingTime = overallCookingTime;
        float stageTime = overallCookingTime / 3f;

        // Update the countdown timer in real time
        while (remainingTime > 0)
        {
            timerText.text = FormatTime(remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "00:00:00";

        // Stage 1: Leave as is
        yield return new WaitForSeconds(stageTime);

        // Stage 2: Medium cooked
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && mediumCookedMaterial != null)
        {
            renderer.material = mediumCookedMaterial;
        }
        yield return new WaitForSeconds(stageTime);

        // Stage 3: Fully cooked
        if (renderer != null && cookedMaterial != null)
        {
            renderer.material = cookedMaterial;
        }
        timeUpAudio.Play();

    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:00}:{seconds:00}:{milliseconds / 10:00}";
    }
}
