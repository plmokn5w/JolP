
using UnityEngine;

[CreateAssetMenu(fileName = "ScarecrowControlModuleData", menuName = "Scriptable Objects/Modules/Control/ScarecrowControlModuleData")]
public class ScarecrowControlModuleData : ControlModuleData
{
    public override Module CreateModule()
    {
        ScarecrowControlModule module = new ScarecrowControlModule();
        module.Initialize(this);
        return module;
    }
}
