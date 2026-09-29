
using UnityEngine;

public class RenderModule : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void LateUpdate()
    {
        // Parent의 Z 깊이 위치를 자식의 Local Y 오프셋으로 반영
        float yOffset = transform.position.z * WorldConfig.Instance.ZtoY;

        spriteTransform.localPosition = new Vector3(0f, yOffset, 0f);
    }

    public void Lookat(float dir)
    {
        if (dir < 0f)
            spriteRenderer.flipX = true;
        if (dir > 0f)
            spriteRenderer.flipX = false;
    }
}