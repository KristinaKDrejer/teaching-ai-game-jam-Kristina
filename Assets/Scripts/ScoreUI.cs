using UnityEngine;
using TMPro;
using Unity.Profiling.LowLevel;
using System;
using JetBrains.Annotations;

public class ScoreUI : MonoBehaviour
{

    public int companyValue = 0;
    public TextMeshProUGUI valueText;

    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;

    

    void Start()
    {

        UpdateUI();


    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Enemy")
        {
            companyValue -= 10;
            valueText.text = "Company Value = " + companyValue;

            //SoundFXManager.Instance.PlaySoundFXClip(positiveValueSound, transform, 0.2f);
            UpdateUI();
            Destroy(other.gameObject);

        }

        if (other.transform.tag == "GoodEnemy")
        {
            companyValue += 10;
            valueText.text = "Company Value = " + companyValue;

            //SoundFXManager.Instance.PlaySoundFXClip(negativeValueSound, transform, 0.2f);

            UpdateUI();

            Destroy(other.gameObject);
            Debug.Log(companyValue);
        }

        if (other.TryGetComponent(out Prompt prompt))
        {
            int severity = prompt.GetSeverity();

            double severityScore = CalculateSeverityScore(severity);


            companyValue += (int)severityScore;
            UpdateUI();

            Debug.Log("Severity: " + severity + ", Score: " + severityScore + ", Company Value: " + companyValue);
        }


    }

    private double CalculateSeverityScore(int severity) 
    {
    severity = Math.Clamp(severity, 0, 3);
        return (-12.5 * severity) + 7.5;

    
    }


   
    void UpdateUI()
    {
        if (valueText != null) { 
        valueText.text = "Company Value = " + companyValue.ToString();

    }


    }
}
