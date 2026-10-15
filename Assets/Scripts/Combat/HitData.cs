
using System;
using UnityEngine;

[Serializable]
public struct HitData
{
    public Vector3 knockbackDirection;
    public float knockbackForce;
    public float hitstunTime;
    public CombatState targetState;
    public float damage;
}
