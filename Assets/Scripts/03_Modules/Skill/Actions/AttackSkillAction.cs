
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class AttackSkillAction : SkillAction
{
    [SerializeField] private HitData hitData;

    private StatModule statModule;

    public override void Initialize(ModuleFrame frame)
    {
        statModule = frame.GetModule<StatModule>();
    }

    public override void Action(List<ModuleFrame> targets)
    {
        foreach (var target in targets)
        {
            if (target is Hittable)
            {
                Hittable hittable = target as Hittable;
                hittable.Hit(hitData);
            }
            else if (target is Damageable)
            {
                Damageable damageable = target as Damageable;
                damageable.TakeDamage(hitData.damageData);
            }
        }
    }
}
