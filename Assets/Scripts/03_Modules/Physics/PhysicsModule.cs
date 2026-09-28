
using UnityEngine;

public class PhysicsModule : Module
{
    public PhysicsState State { get; private set; }
    public Vector3 Velocity { get; private set; }

    private Transform transform;
    private Rigidbody rigidbody;

    private Vector2 targetVelocity;
    private Vector2 internalVelocity;
    private float movePower;

    private Vector2 impulseVelocity;
    private float drag;

    private Vector2 continuousVelocity;

    private float verticalVelocity;
    private float gravity;

    private LayerMask groundLayer;
    private float sphereRadius;
    private float castDistance;
    private Vector3 castOriginOffset;

    public void Initialize(PhysicsModuleData data)
    {
        State = PhysicsState.Aerial;
        movePower = data.movePower;
        groundLayer = data.groundLayer;
        gravity = 10f;
        drag = 100f;
    }

    public override void Initialize(ModuleFrame frame)
    {
        base.Initialize(frame);
        transform = frame.transform;
        if (!frame.TryGetComponent(out rigidbody))
            Debug.Log("프레임에 리지드바디 컴포넌트가 없는데스웅;;");
        sphereRadius = transform.localScale.x * 0.25f;
        castDistance = 0.1f;
        castOriginOffset = new Vector3(0f, transform.localScale.y * 0.25f, 0f);
    }

    public void Move(Vector2 dir, float speed) { targetVelocity = dir.normalized * speed; }

    public void Dash(Vector2 dir, float speed) { internalVelocity = targetVelocity = dir.normalized * speed; }

    public void Launch(float force) { verticalVelocity = force; }

    public void AddImpulseForce(Vector2 dir, float force) { impulseVelocity += dir.normalized * force; }

    public void AddContinousForce(Vector2 dir, float force) { continuousVelocity += dir.normalized * force * Time.deltaTime; }

    public void Teleport(Vector3 position) { transform.position = position; }

    public void SetGravity(float gravty) { gravity = gravty; }

    public void ResetVelocity()
    {
        Velocity = Vector3.zero;
        targetVelocity = Vector2.zero;
        internalVelocity = Vector2.zero;
        impulseVelocity = Vector2.zero;
        continuousVelocity = Vector2.zero;
        verticalVelocity = 0f;
    }

    public void OnFixedUpdate()
    {
        CheckEnviroment();
        HandleMovement();
        HandleGravity();
        ApplyVelocity();
        HandleDampingVelocity();
    }

    private void CheckEnviroment()
    {
        Vector3 origin = transform.position + castOriginOffset;
        if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, castDistance, groundLayer) && verticalVelocity <= 0f)
        {
            State = PhysicsState.Ground;
            gravity = WorldConfig.Instance.Gravity;
        }
        else
            State = PhysicsState.Aerial;
    }

    private void HandleMovement() { internalVelocity = Vector2.MoveTowards(internalVelocity, targetVelocity, movePower * Time.fixedDeltaTime); }

    private void HandleGravity()
    {
        if (State == PhysicsState.Ground && verticalVelocity <= 0f)
            verticalVelocity = -0.5f;
        else
        {
            float gravityScale = verticalVelocity <= 0f ? 3f : 1f;
            verticalVelocity -= gravity * gravityScale * Time.fixedDeltaTime;
        }
    }

    private void ApplyVelocity()
    {
        Vector2 horizontalVelocity = internalVelocity + impulseVelocity + continuousVelocity;
        Velocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.y);
        rigidbody.linearVelocity = Velocity;
    }

    private void HandleDampingVelocity()
    {
        if (State == PhysicsState.Ground)
            impulseVelocity = Vector2.MoveTowards(impulseVelocity, Vector2.zero, drag * Time.fixedDeltaTime);
        continuousVelocity = Vector2.zero;
    }
}
