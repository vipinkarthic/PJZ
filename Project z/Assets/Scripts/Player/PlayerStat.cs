using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : IStat
{
    public float baseValue;
    public float currentValue;
    public float maxValue;

    public void Initialize(float baseValue)
    {
        this.baseValue = baseValue;
        currentValue = baseValue;
        maxValue = baseValue;
    }

    public void ResetStats()
    {
        currentValue = baseValue;
    }

    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
    }

    public void AddValue(float value)
    {
        currentValue = Mathf.Clamp(currentValue + value, 0, maxValue);
    }

    public void SubtractValue(float value)
    {
        currentValue = Mathf.Clamp(currentValue - value, 0, maxValue);
    }

}
