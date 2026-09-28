
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsModuleData", menuName = "Scriptable Objects/Modules/PhysicsModuleData")]
public class PhysicsModuleData : ModuleData
{
    public float movePower;

    public LayerMask groundLayer;

    public override Module CreateModule()
    {
        PhysicsModule module = new PhysicsModule();
        module.Initialize(this);
        return module;
    }
}
