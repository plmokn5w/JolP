
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderModule : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (parentTransform == null) return;

        // Parent의 Z 깊이 위치를 자식의 Local Y 오프셋으로 반영
        float yOffset = parentTransform.position.z * WorldConfig.Instance.ZtoY;

        transform.localPosition = new Vector3(0f, yOffset, 0f);
    }

    public void Lookat(float dir)
    {
        if (dir < 0f)
            spriteRenderer.flipX = true;
        if (dir > 0f)
            spriteRenderer.flipX = false;
    }
}