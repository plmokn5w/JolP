
using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [field: SerializeField] public float Value { get; private set; }

    public static implicit operator float(Stat stat) { return stat.Value; }
}
