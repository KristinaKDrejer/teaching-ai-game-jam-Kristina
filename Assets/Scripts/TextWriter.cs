using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class TextWriter
{
    private const string HTML_ALPHA = "<color=#00000000>";

    private static readonly Dictionary<TextMeshProUGUI, CancellationTokenSource> _writingTokens = new Dictionary<TextMeshProUGUI, CancellationTokenSource>();

    public static async Task WriteText(this TextMeshProUGUI field, string text, float writingSpeed)
    {
        CancellationTokenSource source = _writingTokens.ContainsKey(field) ? _writingTokens[field] : null;

        if (source != null)
        {
            source.Cancel();
            source.Dispose();
            _writingTokens.Remove(field);
        }

        source = new CancellationTokenSource();
        _writingTokens[field] = source;
        CancellationToken token = source.Token;

        field.text = string.Empty;
        int speedMilliseconds = Mathf.RoundToInt(writingSpeed * 1000);

        string displayedLine;
        int alphaIndex = 0;

        try
        {
            foreach(char letter in text.ToCharArray())
            {
                token.ThrowIfCancellationRequested();
                alphaIndex++;
                displayedLine = text.Substring(0, alphaIndex);
                string hiddenLine = HTML_ALPHA + text.Substring(alphaIndex) + "</color>";
                field.text = displayedLine + hiddenLine;

                await Task.Delay(speedMilliseconds);
            }   
        }
        catch (OperationCanceledException)
        {
            source.Dispose();
            _writingTokens.Remove(field);
            return;
        }
        finally
        {
            source.Dispose();
            _writingTokens.Remove(field);
        }

        field.text = text;
    }

    public static async Task ClearText(this TextMeshProUGUI field, float clearingSpeed)
    {
        CancellationTokenSource source = _writingTokens.ContainsKey(field) ? _writingTokens[field] : null;

        if (source != null)
        {
            source.Cancel();
            source.Dispose();
            _writingTokens.Remove(field);
        }

        source = new CancellationTokenSource();
        _writingTokens[field] = source;
        CancellationToken token = source.Token;

        string text = field.text;
        int speedMilliseconds = Mathf.RoundToInt(clearingSpeed * 1000);

        string displayedLine;
        int alphaIndex = text.Length;

        try
        {
            while (alphaIndex > 0)
            {
                token.ThrowIfCancellationRequested();
                alphaIndex--;
                displayedLine = text.Substring(0, alphaIndex);
                string hiddenLine = HTML_ALPHA + text.Substring(alphaIndex) + "</color>";
                field.text = displayedLine + hiddenLine;

                await Task.Delay(speedMilliseconds);
            }
        }
        catch (OperationCanceledException)
        {
            source.Dispose();
            _writingTokens.Remove(field);
            return;
        }
        finally
        {
            source.Dispose();
            _writingTokens.Remove(field);
            field.text = string.Empty;
        }
    }
}
