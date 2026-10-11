
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
        CurValue = Mathf.MoveTowards(CurValue, 0f, loss);
        //ValueChangeChannel.OnNext(this);
    }

    public void Recover(float recovery)
    {
        CurValue = Mathf.MoveTowards(CurValue, MaxValue, recovery);
        //ValueChangeChannel.OnNext(this);
    }

    public void SetMaxValue(float maxValue)
    {
        MaxValue = maxValue;
    }
}
