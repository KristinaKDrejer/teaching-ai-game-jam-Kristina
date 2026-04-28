using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AiCenter : MonoBehaviour
{
 
    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;
    [SerializeField] private ScoreUI scoreUI;


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.TryGetComponent(out Prompt prompt))
        {
            int severity = prompt.GetSeverity();

            float severityScore = CalculateSeverityScore(severity);

            Debug.Log("Severity: " + severity + ", Score: " + severityScore.ToString("F2"));

            scoreUI.UpdateScore(severityScore);
        }

        

    }

    

  

    private float CalculateSeverityScore(int severity)
    {
        severity = Math.Clamp(severity, 0, 3);

        return (-12.5f * severity) + 7.5f;
    }
}

