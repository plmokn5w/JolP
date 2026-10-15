
using UnityEngine;
using R3;

public class CombatModule : MonoBehaviour
{
    public ReactiveProperty<CombatState> State { get; private set; } = new();
    [field: SerializeField] public float HitstunTimer = 0.0f;

    public void SetState(CombatState state)
    {
        State.Value = state;
    }

    public void SetHitstun(float time)
    {
        HitstunTimer = time;
    }

    public void AddHitstun(float time)
    {
        HitstunTimer += time;
    }

    private void Awake()
    {
        State.Value = CombatState.Neutral;
    }

    private void Update()
    {
        if (HitstunTimer == 0f)
            return;
        HitstunTimer -= Time.deltaTime;
        if (HitstunTimer <= 0f)
        {
            HitstunTimer = 0f;
            State.Value = CombatState.Neutral;
        }
    }
}