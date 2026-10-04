
using System;
using UnityEngine;

[Serializable]
public class Gauge
{
    [field: SerializeField] public float CurValue { get; private set; }
    [field: SerializeField] public float MaxValue { get; private set; }

    public static implicit operator float(Gauge gauge) { return gauge.CurValue; }

    public void Lose(float loss)
    {
        CurValue -= loss;
        //ValueChangeChannel.OnNext(this);
    }

    public void Recover(float recovery)
    {
        CurValue += recovery;
        //ValueChangeChannel.OnNext(this);
    }

    public void SetMaxValue(float maxValue)
    {
        MaxValue = maxValue;
    }
}
