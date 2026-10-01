
using UnityEngine;

public class Player : MonoBehaviour
{
    private PhysicsModule physicsModule;
    private RenderModule renderModule;
    private StatModule statModule;

    private void Awake()
    {
        physicsModule = GetComponent<PhysicsModule>();
        renderModule = GetComponent<RenderModule>();
        statModule = GetComponent<StatModule>();
    }

    private void Update()
    {
        // 1. 입력 수집 (X: 좌우, Z: 앞뒤 깊이)
        Vector2 moveInput = WorldConfig.Instance.Input.Player.Move.ReadValue<Vector2>();
        float inputX = moveInput.x;

        physicsModule.Move(moveInput, statModule[StatType.MoveSpeed]);

        // 3. 점프 입력 받기
        if (WorldConfig.Instance.Input.Player.Jump.WasPressedThisFrame() && physicsModule.IsGrounded)
            physicsModule.Jump(statModule[StatType.JumpPower]);

        // 4. 이동 방향에 따른 스프라이트 좌우 반전
        renderModule.Lookat(inputX);
    }
}