
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Wall leftWall;
    [SerializeField] private Wall rightWall;

    [Header("벽 바운스 설정")]
    [SerializeField] private HitData wallBounceHitData;

    public void OnBoundaryTriggered(Bounceable bounceable, float xDir)
    {
        HitData hitData = wallBounceHitData;
        Vector3 dir = hitData.knockbackDirection;
        dir.x *= xDir;
        hitData.knockbackDirection = dir;

        bounceable.ApplyBounce(hitData);
    }
}
