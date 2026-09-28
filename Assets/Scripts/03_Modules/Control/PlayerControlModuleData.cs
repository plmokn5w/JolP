
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControlModuleData", menuName = "Scriptable Objects/Modules/Control/PlayerControlModuleData")]
public class PlayerControlModuleData : ControlModuleData
{
    public static InputSystem input = null;

    public override Module CreateModule()
    {
        if (input == null)
        {
            input = new InputSystem();
            input.Enable();
        }
        PlayerControlModule module = new PlayerControlModule();
        module.Initialize(this);
        return module;
    }
}
