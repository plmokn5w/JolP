
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

        float parentY = parentTransform.position.y;
        float parentZ = parentTransform.position.z;

        float shadowYOffset = (parentZ * WorldConfig.Instance.ZtoY) - parentY;
        transform.localPosition = new Vector3(0f, shadowYOffset, 0f);

        float jumpProgress = Mathf.Clamp01(parentY / 5.0f);
        transform.localScale = Vector3.Lerp(initialScale, initialScale * 0.5f, jumpProgress);

        Color color = shadowSprite.color;
        color.a = Mathf.Lerp(0.6f, 0.2f, jumpProgress);
        shadowSprite.color = color;
    }
}