
using UnityEngine;

[CreateAssetMenu(fileName = "CombatModuleData", menuName = "Scriptable Objects/Modules/CombatModuleData")]
public class CombatModuleData : ModuleData
{
    public override Module CreateModule()
    {
        CombatModule module = new CombatModule();
        module.Initialize(this);
        return module;
    }
}
