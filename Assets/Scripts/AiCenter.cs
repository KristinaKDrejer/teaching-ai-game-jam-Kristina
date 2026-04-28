using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class AiCenter : MonoBehaviour
{
    private AiCenterData aiCenterData;

    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;
    [SerializeField] private ScoreUI scoreUI;

    private void Awake()
    {
        SetData(CreateData());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        int childCount = other.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = other.transform.GetChild(i);

            if (child.TryGetComponent(out Prompt prompt))
            {
                int severity = prompt.GetSeverity();
                severity += aiCenterData.GDPR ? 1 : 0;

                float severityScore = CalculateSeverityScore(severity);

                Debug.Log("Severity: " + severity + ", Score: " + severityScore.ToString("F2"));

                ScoreUI.Instance.UpdateScore(severityScore);
                UpdateSize(GetSizeChange(severity));
            }
        }

        Destroy(other.gameObject);
    }

    private void UpdateSize(Vector3 sizeChange)
    {
        transform.localScale += sizeChange;

        Vector3 endSize = Vector3.one * aiCenterData.EndSize;
        float difference = Vector3.Distance(endSize, transform.localScale);
        if (difference < 0)
        {
            SetData(CreateData());
        }
    }

    private Vector3 GetSizeChange(int severity)
    {
        if (severity == 0)
        {
            return Vector3.one * 0.1f;

        }

        return Vector3.one * -(0.05f * severity);
    }

    private AiCenterData CreateData()
    {
        float sizeMult = UnityEngine.Random.Range(0.75f, 2.25f);
        float endDifference = UnityEngine.Random.Range(0.5f, 1.5f);
        float endSize = Math.Max(sizeMult - endDifference, 0.5f);
        float randomGDPR = UnityEngine.Random.Range(0, 100f);

        AiCenterData data = new AiCenterData
        {
            SizeMult = sizeMult,
            EndSize = endSize,
            GDPR = randomGDPR < 90
        };
        return data;
    }

    private void SetData(AiCenterData aiCenterData)
    {
        this.aiCenterData = aiCenterData;
        transform.localScale *= aiCenterData.SizeMult;
    }
    
    private float CalculateSeverityScore(int severity)
    {
        return (-12.5f * severity) + 7.5f;
    }
}

public struct AiCenterData
{
    public float SizeMult;
    public float EndSize;
    public bool GDPR;
}

