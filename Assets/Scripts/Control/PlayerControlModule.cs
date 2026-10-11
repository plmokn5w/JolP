
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerControlModule : ControlModule
{
    [SerializeField] private Dictionary<InputActionReference, CommandFlags> flags;
    [SerializeField] private InputActionReference moveInputRef;
    private InputAction moveInput;
    
    private List<(InputAction, CommandFlags)> bindings;

    private void Awake()
    {
        bindings = new List<(InputAction, CommandFlags)>();
        foreach (var (key, value) in flags)
            bindings.Add((key, value));
        moveInput = moveInputRef;
    }

    private void Update()
    {
        CommandFlags flags = CommandFlags.None;

        foreach (var (input, commmand) in bindings)
        {
            if (input.WasPressedThisFrame())
                flags |= commmand;
        }

        Vector2 moveDirection = moveInput.ReadValue<Vector2>();

        if (moveDirection.sqrMagnitude > 0.01f)
            flags |= CommandFlags.Move;

        Flags = flags;

        MoveDirection = moveDirection;

        float xDir = moveDirection.x;
        if (xDir < 0f)
            LookDirection.Value = FacingSide.Left;
        if (xDir > 0f)
            LookDirection.Value = FacingSide.Right;
    }
}
