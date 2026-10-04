
using UnityEngine;

public class WorldConfig : MonoBehaviour
{
    public static WorldConfig Instance;

    public InputSystem Input { get; private set; }
    [field : SerializeField] public LayerMask GroundLayer { get; private set; }
    [field : SerializeField] public Vector3 GroundCheckSize { get; private set; }
    [field : SerializeField] public float Gravity { get; private set; }
    [field : SerializeField] public float FallingGravityWeight { get; private set; }
    [field : SerializeField] public float Drag { get; private set; }
    [field : SerializeField] public float ZtoY { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Input = new InputSystem();
            Input.Enable();
            Instance = this;
        }
    }
}
