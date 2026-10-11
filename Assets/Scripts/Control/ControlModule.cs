
using UnityEngine;
using R3;
using Alchemy.Inspector;

public abstract class ControlModule : MonoBehaviour
{
    [field: SerializeField] public CommandFlags Flags { get; protected set; }
    public ReactiveProperty<FacingSide> LookDirection { get; protected set; } = new ReactiveProperty<FacingSide>();
    [field: SerializeField] public Vector2 MoveDirection { get; protected set; }
}
