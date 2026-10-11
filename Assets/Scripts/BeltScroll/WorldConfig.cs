
using UnityEngine;
using System.Collections.Generic;

public class WorldConfig : MonoBehaviour
{
    public static WorldConfig Instance;

    public InputSystem Input { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    [field: SerializeField] public Vector3 GroundCheckSize { get; private set; }
    [field: SerializeField] public float Gravity { get; private set; }
    [field: SerializeField] public float FallingGravityWeight { get; private set; }
    [field: SerializeField] public float DragFactor { get; private set; }
    [field: SerializeField] public float ZtoY { get; private set; }
    public Dictionary<StateKey, int> AnimationHash { get; private set; } = new Dictionary<StateKey, int>();
    [SerializeField] private Dictionary<StateKey, string> animNames;

    private void Awake()
    {
        if (Instance == null)
        {
            Input = new InputSystem();
            Input.Enable();
            foreach (var (key, name) in animNames)
                AnimationHash.Add(key, Animator.StringToHash(name));
            Instance = this;
        }
    }
}
