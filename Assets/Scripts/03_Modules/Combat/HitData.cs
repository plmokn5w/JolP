
using System;
using UnityEngine;

[Serializable]
public class HitData
{
    public CombatState prevState;
    public CombatState nextState;
    public Vector3 knockbackDirection;
    public float knockbackForce;
    public float hitStunDuration;
    public DamageData damageData;
}
