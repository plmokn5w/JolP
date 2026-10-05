
using UnityEngine;

public abstract class ControlModule : MonoBehaviour
{
    [field: SerializeField] public CommandFlags Flags { get; protected set; }
    [field : SerializeField] public FacingSide LookDirection { get; protected set; }
    [field: SerializeField] public Vector2 MoveDirection { get; protected set; }

    public void Lookat(FacingSide facingSide) { LookDirection = facingSide; }
}
