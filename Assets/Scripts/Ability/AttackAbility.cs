
using System;
using UnityEngine;

[Serializable]
public class AttackAbility : Ability
{
    private ControlModule controlModule;
    private CombatModule combatModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        controlModule = gameObject.GetComponent<ControlModule>();
        combatModule = gameObject.GetComponent<CombatModule>();
    }

    public override bool CanActivate()
    {
        return controlModule.Flags.HasFlag(CommandFlags.Attack);
    }

    public override void Execute()
    {
        combatModule.PerformAttack();
    }
}
