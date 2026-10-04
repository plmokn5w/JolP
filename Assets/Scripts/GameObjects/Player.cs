
using UnityEngine;

public class Player : MonoBehaviour
{
    private PhysicsModule physicsModule;
    private RenderModule renderModule;
    private StatModule statModule;
    private CombatModule combatModule;
    private AbilityModule abilityModule;

    private void Awake()
    {
        physicsModule = GetComponent<PhysicsModule>();
        renderModule = GetComponent<RenderModule>();
        statModule = GetComponent<StatModule>();
        combatModule = GetComponent<CombatModule>();
        abilityModule = GetComponent<AbilityModule>();
    }

    private void Update()
    {
        Vector2 moveInput = WorldConfig.Instance.Input.Player.Move.ReadValue<Vector2>();
        float inputX = moveInput.x;

        renderModule.Lookat(inputX);
        combatModule.SetFacingDirection(inputX);

        if (WorldConfig.Instance.Input.Player.Jump.WasPressedThisFrame() && physicsModule.State == PhysicsState.Ground)
            physicsModule.Launch(statModule[StatType.JumpPower]);

        if (WorldConfig.Instance.Input.Player.Attack.WasPressedThisFrame())
            combatModule.PerformAttack();
    }
}