
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsModule : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("현재 속도")]
    [SerializeField] private Vector3 velocity;

    private Vector2 internalVelocity;
    private float verticalVelocity;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckEnvironment();

        HandleGravity();

        ApplyVelocity();
    }

    private void CheckEnvironment()
    {
        Vector3 checkPos = transform.position;
        Vector3 halfExtents = Vector3.Scale(WorldConfig.Instance.GroundCheckSize, transform.localScale) * 0.5f;

        IsGrounded = Physics.CheckBox(
            checkPos,
            halfExtents,
            transform.rotation,
            WorldConfig.Instance.GroundLayer
        ) && verticalVelocity <= 0f;
    }

    private void HandleGravity()
    {
        if (IsGrounded)
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
        velocity = new Vector3(internalVelocity.x, verticalVelocity, internalVelocity.y);
        rigidbody.linearVelocity = velocity;
    }

    public void Move(Vector2 direction, float speed)
    {
        internalVelocity = direction.normalized * speed;
    }

    public void Jump(float jumpPower)
    {
        verticalVelocity = jumpPower;
    }

    // 씬 뷰에서 접지 판정 범위를 시각적으로 확인
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Vector3 checkPos = transform.position;
        Vector3 halfExtents = Vector3.Scale(WorldConfig.Instance.GroundCheckSize, transform.localScale) * 0.5f;

        Gizmos.matrix = Matrix4x4.TRS(checkPos, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);
    }
}
