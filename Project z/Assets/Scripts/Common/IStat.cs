using System;

public interface IStat
{
    void Initialize(float baseValue);
    void ResetStats();
    void SetValue(float value);
    void AddValue(float value);
    void SubtractValue(float value);
}