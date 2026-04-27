using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationController))]
public class Prompt : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private TextMeshProUGUI _checkText;
    [SerializeField] private RawImage _approvalIndicator;
    [SerializeField] private PromptData _promptData;

    private AnimationController _animationController;
    [SerializeField] private AnimationClip _expandAnimation;
    [SerializeField] private AnimationClip _shrinkAnimation;

    private CancellationTokenSource _writingCancellationTokenSource = null;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();

       if (_promptData != null) SetData(_promptData);
    }

    public void SetData(PromptData promptData)
    {
        _checkText.text = string.Empty;
        SetPrompt(promptData.PromptHeader);
        SetApproval(promptData.IndicatorType);
    }

    public int GetSeverity()
    {
        if (_promptData == null) return 0;
        return _promptData.Severity;
    }

    private async void SetPrompt(string promptText)
    {
        await _promptText.WriteText(promptText, 0.05f);
    }

    private void SetApproval(IndicatorTypes indicatorType)
    {
        Texture2D texture = indicatorType.GetIndicator();
        if (texture != null)
        {
            _approvalIndicator.texture = texture;
        }

        if (indicatorType == IndicatorTypes.Check)
        {
            _promptData.SetRandomCheck();
        }
    }

    private async void Expand()
    {
        await _animationController.PlayAnimationClipAsync(_expandAnimation);
        PromptData.PromptCheck check = _promptData.GetCurrentCheck();
        await _checkText.WriteText(check.CheckText, 0.005f);
    }

    private async void Shrink()
    {
        await _checkText.ClearText(0.0025f);
        await _animationController.PlayAnimationClipAsync(_shrinkAnimation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with " + collision.name);

        if (collision.CompareTag("Player") && _promptData.Expandable)
        {
            Expand();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _promptData.Expandable)
        {
            Shrink();
        }
    }
}
