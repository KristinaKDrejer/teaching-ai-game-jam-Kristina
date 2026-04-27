using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Prompt : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private RawImage _approvalIndicator;
    [SerializeField] private PromptData _promptData;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

       if (_promptData != null) SetData(_promptData);
    }

    public void SetData(PromptData promptData)
    {
        SetPrompt(promptData.PromptHeader);
        SetApproval(promptData.IndicatorType);
    }

    private void SetPrompt(string promptText)
    {
        _promptText.text = promptText;
    }

    private void SetApproval(IndicatorTypes indicatorType)
    {
        Texture2D texture = indicatorType.GetIndicator();
        if (texture != null)
        {
            _approvalIndicator.texture = texture;
        }
    }

    private void Expand()
    {

    }

    private void Shrink()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
