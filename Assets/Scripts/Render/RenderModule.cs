
using UnityEngine;

public class RenderModule : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Lookat(FacingSide facingSide)
    {
        if (facingSide == FacingSide.Left)
            spriteRenderer.flipX = true;
        if (facingSide == FacingSide.Right)
            spriteRenderer.flipX = false;
    }

    private void LateUpdate()
    {
        float yOffset = transform.position.z * WorldConfig.Instance.ZtoY;

        spriteTransform.localPosition = Vector3.up * yOffset;
    }
}