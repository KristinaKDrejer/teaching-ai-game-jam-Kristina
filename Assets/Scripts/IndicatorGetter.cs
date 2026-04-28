using System;
using System.Collections.Generic;
using UnityEngine;

public enum IndicatorTypes
{
    Approved,
    Disapproved,
    Check,
    Random
}

public static class IndicatorGetter
{
    private static readonly Dictionary<IndicatorTypes,Texture2D> _indicatorDictionary = new Dictionary<IndicatorTypes,Texture2D>();

    private static void Init()
    {
        foreach(IndicatorTypes type in Enum.GetValues(typeof(IndicatorTypes)))
        {
            if (type == IndicatorTypes.Random) continue;

            Texture2D indicatorTexture = Resources.Load<Texture2D>($"Indicators/{type}");
            _indicatorDictionary[type] = indicatorTexture;
        }
    }

    public static Texture2D GetIndicator(this IndicatorTypes indicatorType)
    {
        if (_indicatorDictionary.Count == 0) Init();

        if (indicatorType == IndicatorTypes.Random)
        {
            int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(IndicatorTypes)).Length - 1);
            IndicatorTypes randomType = (IndicatorTypes)random;
            return _indicatorDictionary[randomType];
        }

        return _indicatorDictionary[indicatorType];
    
    }
}
