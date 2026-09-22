using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BeltScrollPlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 7.0f;

    [Header("접지 판정 설정")]
    [SerializeField] private Vector3 groundCheckOffset; // 발밑 접지점 위치 (비어있으면 Parent 원점 사용)
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("자식 비주얼 참조")]
    [SerializeField] private Transform spriteTransform; // 방향 전환(Flip)용 자식 스프라이트 Transform

    private InputSystem input;

    private Rigidbody rb;
    private Vector2 inputVector;
    private bool isGrounded;
    private bool jumpRequested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 물리 회전 방지 및 3D 중력 활성화
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = true;
        input = new InputSystem();
        input.Enable();
    }

    private void Update()
    {
        // 1. 입력 수집 (X: 좌우, Z: 앞뒤 깊이)
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        float inputX = moveInput.x;
        float inputZ = moveInput.y;

        // 대각선 이동 시 속도 증가를 막기 위해 정규화(Normalize)
        inputVector = new Vector2(inputX, inputZ).normalized;

        // 2. 바닥 접지 체크 (SphereCast)
        Vector3 checkPos = transform.position + groundCheckOffset;
        isGrounded = Physics.CheckSphere(checkPos, groundCheckRadius, groundLayer);

        // 3. 점프 입력 받기
        if (input.Player.Jump.WasPressedThisFrame() && isGrounded)
        {
            jumpRequested = true;
        }

        // 4. 이동 방향에 따른 스프라이트 좌우 반전
        HandleFacingDirection(inputX);
    }

    private void FixedUpdate()
    {
        // X축: 좌우 이동, Z축: 깊이 이동, Y축: 기존 물리 속도(중력/점프) 유지
        Vector3 targetVelocity = new Vector3(
            inputVector.x * moveSpeed,
            rb.linearVelocity.y,
            inputVector.y * moveSpeed
        );

        rb.linearVelocity = targetVelocity;

        // 점프 물리 처리
        if (jumpRequested)
        {
            // 순간적인 Y축 속도 초기화 후 순간 충격력(Impulse) 적용
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            jumpRequested = false;
        }
    }

    // 캐릭터 바라보는 방향 처리
    private void HandleFacingDirection(float inputX)
    {
        if (spriteTransform == null) return;

        if (inputX > 0.01f)
        {
            spriteTransform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (inputX < -0.01f)
        {
            spriteTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    // 씬 뷰에서 접지 판정 범위를 시각적으로 확인
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 checkPos = transform.position + groundCheckOffset;
        Gizmos.DrawWireSphere(checkPos, groundCheckRadius);
    }
}