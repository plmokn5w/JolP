
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;

    private SpriteRenderer shadowSprite;
    private Vector3 initialScale;

    void Awake()
    {
        shadowSprite = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (parentTransform == null) return;

        float parentY = parentTransform.position.y; // 점프 높이
        float parentZ = parentTransform.position.z; // 깊이

        // 바닥 위치(Z 오프셋)에는 남아있되, 점프 높이(parentY)만큼 아래로 빼서 바닥에 고정
        float shadowYOffset = (parentZ * WorldConfig.Instance.ZtoY) - parentY;
        transform.localPosition = new Vector3(0f, shadowYOffset, 0f);

        // 점프 높이에 따라 그림자 크기 축소 및 투명도 감소 연출
        float jumpProgress = Mathf.Clamp01(parentY / 5.0f); // 5m 점프 기준
        transform.localScale = Vector3.Lerp(initialScale, initialScale * 0.5f, jumpProgress);

        Color color = shadowSprite.color;
        color.a = Mathf.Lerp(0.6f, 0.2f, jumpProgress);
        shadowSprite.color = color;
    }
}