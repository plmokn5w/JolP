
using UnityEngine;

public class Player : MonoBehaviour
{
    private PhysicsModule physicsModule;
    private RenderModule renderModule;
    private PlayerControlModule playerControlModule;
    private StatModule statModule;
    private CombatModule combatModule;
    private AbilityModule abilityModule;

    private void Awake()
    {
        physicsModule = GetComponent<PhysicsModule>();
        renderModule = GetComponent<RenderModule>();
        playerControlModule = GetComponent<PlayerControlModule>();
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
    }
}