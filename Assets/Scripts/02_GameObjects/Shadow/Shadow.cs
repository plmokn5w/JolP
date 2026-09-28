using UnityEngine;

public class Shadow : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("그림자가 따라다닐 대상 (보통 Player의 부모 객체)")]
    public Transform followTarget;
    public LayerMask groundLayer; // SensorModule과 같은 레이어 사용

    [Header("Shadow Settings")]
    public Vector3 offset = new Vector3(0, 0.02f, 0); // 바닥이랑 겹치지 않게 살짝 띄움
    public float maxDistance = 10f; // 이 높이 이상 올라가면 그림자 사라짐

    [Header("Animation")]
    public bool scaleWithHeight = true; // 높이에 따라 작아지게 할지
    public float minScale = 0.5f; // 가장 높이 떴을 때 크기 배율

    private Vector3 initialScale;
    private Renderer shadowRenderer;

    private void Start()
    {
        // 1. 만약 타겟 설정이 안되어 있다면 부모를 타겟으로 잡음
        if (followTarget == null)
            followTarget = transform.parent;

        // 2. ★핵심: 부모-자식 관계 끊기 (Unparent)
        // 이걸 안 하면 부모가 점프할 때 로컬 좌표계 때문에 같이 딸려 올라감
        transform.SetParent(null);

        initialScale = transform.localScale;
        shadowRenderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            Destroy(gameObject); // 타겟이 죽으면 그림자도 삭제
            return;
        }

        // 캐릭터 위치에서 아래로 Ray 쏘기
        // (캐릭터 중심이 발바닥이라면 살짝 위에서 쏴야 함)
        Vector3 rayOrigin = followTarget.position + Vector3.up * 0.5f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, maxDistance, groundLayer))
        {
            // 땅을 찾음 -> 그림자를 땅 위치로 이동
            transform.position = hit.point + offset;

            // 그림자 회전을 땅의 경사면에 맞춤 (선택사항: 원하면 주석 해제)
            // transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); 
            // 보통 2.5D 횡스크롤에서는 회전 안 하는 게 더 깔끔함 -> Quaternion.identity 유지
            transform.rotation = Quaternion.Euler(90, 0, 0); // Quad(Plane)를 쓴다면 눕혀야 함

            // 그림자 보이기
            if (shadowRenderer != null) shadowRenderer.enabled = true;

            // 높이에 따른 크기 조절 (연출)
            if (scaleWithHeight)
            {
                float distance = hit.distance; // 레이 시작점~바닥 거리
                float scaleRatio = Mathf.Clamp01(1 - (distance / maxDistance));
                // 거리가 멀수록 작아짐 (최소 minScale 까지)
                float finalScale = Mathf.Lerp(minScale, 1f, scaleRatio);

                transform.localScale = initialScale * finalScale;
            }
        }
        else
        {
            // 땅이 너무 멀거나 없으면 그림자 숨김
            if (shadowRenderer != null) shadowRenderer.enabled = false;
        }
    }
}