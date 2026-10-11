
using R3;

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

    private void Start()
    {
        stateMachine.OnStateEnter.Subscribe(nextState => renderModule.RenderState(nextState));
        playerControlModule.LookDirection.Subscribe(facingSide => renderModule.Lookat(facingSide));
    }
}