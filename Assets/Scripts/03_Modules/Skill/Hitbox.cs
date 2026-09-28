
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public abstract class Hitbox
{
    [field: SerializeField] public float Time { get; protected set; }
    [field: SerializeField] public Vector3 Offset { get; protected set; }
    public abstract void ScanTargets(Vector3 origin, FacingDirection facingDirection, List<ModuleFrame> targets);

    public virtual Hitbox Clone()
    {
        Hitbox clone = MemberwiseClone() as Hitbox;
        return clone;
    }
}
