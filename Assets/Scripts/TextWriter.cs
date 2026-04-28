using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class TextWriter
{
    private const string HTML_ALPHA = "<color=#00000000>";

    public static async Task WriteText(this TextMeshProUGUI field, string text, float writingSpeed, CancellationToken cancellationToken = default)
    {
        field.text = string.Empty;
        int speedMilliseconds = Mathf.RoundToInt(writingSpeed * 1000);

        string displayedLine;
        int alphaIndex = 0;

        try
        {
            foreach(char letter in text.ToCharArray())
            {
                alphaIndex++;
                displayedLine = text.Substring(0, alphaIndex);
                string hiddenLine = HTML_ALPHA + text.Substring(alphaIndex) + "</color>";
                field.text = displayedLine + hiddenLine;

                await Task.Delay(speedMilliseconds, cancellationToken);
            }   
        }
        catch (OperationCanceledException)
        {
            return;
        }
        
        field.text = text;
    }

    public static async Task ClearText(this TextMeshProUGUI field, float clearingSpeed, CancellationToken cancellationToken = default)
    {
        string text = field.text;
        int speedMilliseconds = Mathf.RoundToInt(clearingSpeed * 1000);

        string displayedLine;
        int alphaIndex = text.Length;

        try
        {
            while (alphaIndex > 0)
            {
                alphaIndex--;
                displayedLine = text.Substring(0, alphaIndex);
                string hiddenLine = HTML_ALPHA + text.Substring(alphaIndex) + "</color>";
                field.text = displayedLine + hiddenLine;

                await Task.Delay(speedMilliseconds, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            return;
        }
        
        field.text = string.Empty;
    }
}
