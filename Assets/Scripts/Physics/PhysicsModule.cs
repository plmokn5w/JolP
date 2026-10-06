
using UnityEngine;

public class PhysicsModule : MonoBehaviour
{
    public PhysicsState State { get; private set; }

    private Rigidbody rigidbody;

    [Header("현재 속도")]
    [SerializeField] private Vector3 velocity;

    private Vector2 internalVelocity;
    private Vector2 impulseVelocity;
    private Vector2 continuousVelocity;
    private float verticalVelocity;

    public void Move(Vector2 dir, float speed) { internalVelocity = dir.normalized * speed; }
    public void Launch(float force) { verticalVelocity = force; }
    public void SetImpulseForce(Vector2 dir, float force) { impulseVelocity = dir.normalized * force; }
    public void AddImpulseForce(Vector2 dir, float force) { impulseVelocity += dir.normalized * force; }
    public void AddContinousForce(Vector2 dir, float force) { continuousVelocity += dir.normalized * force * Time.deltaTime; }
    public void Teleport(Vector3 position) { transform.position = position; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckEnvironment();
        HandleGravity();
        ApplyVelocity();
        HandleDampingVelocity();
    }

    private void CheckEnvironment()
    {
        Vector3 checkPos = transform.position;
        Vector3 halfExtents = Vector3.Scale(WorldConfig.Instance.GroundCheckSize, transform.localScale) * 0.5f;

        if (Physics.CheckBox(checkPos, halfExtents, transform.rotation, WorldConfig.Instance.GroundLayer) && verticalVelocity <= 0f)
            State = PhysicsState.Ground;
        else
            State = PhysicsState.Aerial;
    }

    private void HandleGravity()
    {
        if (State == PhysicsState.Ground)
        {
            verticalVelocity = 0f;
            return;
        }

        float gravity = WorldConfig.Instance.Gravity;

        if (verticalVelocity < 0f)
            gravity *= WorldConfig.Instance.FallingGravityWeight;

        verticalVelocity -= gravity * Time.fixedDeltaTime;
    }

    private void ApplyVelocity()
    {
        Vector2 horizontalVelocity = internalVelocity + impulseVelocity + continuousVelocity;
        velocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.y);
        rigidbody.linearVelocity = velocity;
    }

    private void HandleDampingVelocity()
    {
        if (State == PhysicsState.Ground)
            impulseVelocity = Vector2.MoveTowards(impulseVelocity, Vector2.zero, WorldConfig.Instance.Drag * Time.fixedDeltaTime);
        continuousVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (WorldConfig.Instance == null) return;

        Gizmos.color = State == PhysicsState.Ground ? Color.green : Color.red;
        Vector3 checkPos = transform.position;
        Vector3 halfExtents = Vector3.Scale(WorldConfig.Instance.GroundCheckSize, transform.localScale) * 0.5f;

        Gizmos.matrix = Matrix4x4.TRS(checkPos, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);
    }
}
