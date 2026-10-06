
using UnityEngine;

public class Player : Entity
{
    private PhysicsModule physicsModule;
    private RenderModule renderModule;
    private PlayerControlModule playerControlModule;
    private StatModule statModule;
    private CombatModule combatModule;
    private StateMachine stateMachine;
    private AbilityModule abilityModule;

    private void Awake()
    {
        physicsModule = GetComponent<PhysicsModule>();
        renderModule = GetComponent<RenderModule>();
        playerControlModule = GetComponent<PlayerControlModule>();
        statModule = GetComponent<StatModule>();
        combatModule = GetComponent<CombatModule>();
        stateMachine = GetComponent<StateMachine>();
        abilityModule = GetComponent<AbilityModule>();
    }

    private void Update()
    {
        renderModule.Lookat(playerControlModule.LookDirection);
        combatModule.SetFacingDirection(playerControlModule.LookDirection);
    }
}