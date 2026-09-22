using UnityEngine;

public class BeltScrollSpriteOffset : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private float zToYRatio = 0.577f; // 30도 시점 기준 (tan(30°))

    void LateUpdate()
    {
        if (parentTransform == null) return;

        // Parent의 Z 깊이 위치를 자식의 Local Y 오프셋으로 반영
        float yOffset = parentTransform.position.z * zToYRatio;

        transform.localPosition = new Vector3(0f, yOffset, 0f);
    }
}