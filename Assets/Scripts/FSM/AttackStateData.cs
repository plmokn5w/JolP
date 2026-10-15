
using System;
using UnityEngine;

[Serializable]
public class AttackStateData : StateData
{
    public StateKey targetState;
    public LayerMask targetLayer;
    public Vector3 attackBoxSize;
    public Vector3 attackBoxOffset;
    public HitData originHitData;
}
