
using UnityEngine;

[ModuleGroup(typeof(ControlModule))]
public class PlayerControlModule : ControlModule
{
    private InputSystem input;

    public void Initialize(PlayerControlModuleData data) { input = PlayerControlModuleData.input; }

    public override void OnUpdate()
    {
        Command = Command.None;

        MoveDirection = input.Player.Move.ReadValue<Vector2>();

        if (MoveDirection.sqrMagnitude > 0.01f)
            Command |= Command.Move;

        /*
        if (input.Player.Dash.WasPerformedThisFrame())
        {
            Debug.Log("달리기");
            Command |= Command.Run;
        }
        else if (input.Player.Dash.WasReleasedThisFrame())
        {
            Debug.Log("대쉬");
            Command |= Command.Dash;
        }
        */

        if (input.Player.Jump.WasPressedThisFrame())
            Command |= Command.Jump;

        if (input.Player.Attack.WasPressedThisFrame())
            Command |= Command.Attack;

        /*
        if (input.Player.Skill.WasPressedThisFrame())
            Command |= Command.Skill;

        if (input.Player.Switch.WasPressedThisFrame())
            Command |= Command.Switch;
        */
    }
}
