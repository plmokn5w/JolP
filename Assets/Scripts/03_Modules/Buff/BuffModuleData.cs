
using UnityEngine;

[CreateAssetMenu(fileName = "BuffModuleData", menuName = "Scriptable Objects/Modules/BuffModuleData")]
public class BuffModuleData : ModuleData
{
    public override Module CreateModule()
    {
        BuffModule module = new BuffModule();
        module.Initialize(this);
        return module;
    }
}
