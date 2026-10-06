
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
        Flags = CommandFlags.None;

        foreach (var (input, commmand) in bindings)
        {
            if (input.WasPressedThisFrame())
                Flags |= commmand;
        }

        MoveDirection = moveInput.ReadValue<Vector2>();
        float xDir = MoveDirection.x;
        if (xDir < 0f)
            LookDirection = FacingSide.Left;
        if (xDir > 0f)
            LookDirection = FacingSide.Right;
    }
}
