
using R3;

public class CombatModule : Module
{
    public CombatState State { get; private set; }

    public Subject<CombatState> StateChangeChannel { get; private set; }

    public void Initialize(CombatModuleData data)
    {
        State = CombatState.Neutral;
        StateChangeChannel = new Subject<CombatState>();
    }

    public void ChangeState(CombatState nextState)
    {
        State = nextState;
        StateChangeChannel.OnNext(State);
    }

    public void OnUpdate()
    {
    }
}
