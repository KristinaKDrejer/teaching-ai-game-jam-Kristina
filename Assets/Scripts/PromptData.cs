using UnityEngine;

[CreateAssetMenu(fileName = "PromptData", menuName = "Prompt")]
public class PromptData : ScriptableObject
{
    public string PromptHeader;
    public IndicatorTypes IndicatorType;
    public bool Expandable;
}
