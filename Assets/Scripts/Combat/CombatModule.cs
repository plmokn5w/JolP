
using UnityEngine;

public class CombatModule : MonoBehaviour
{
    [Header("공격 판정 설정")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector3 attackBoxSize;
    [SerializeField] private Vector3 attackBoxOffset;
    [SerializeField] private HitData originHitData;

    private float facingDirection = 1f;

    public void SetFacingDirection(float dir)
    {
        if (dir != 0) facingDirection = Mathf.Sign(dir);
    }

    public void PerformAttack()
    {
        Vector3 orientedOffset = attackBoxOffset;
        orientedOffset.x *= facingDirection;

        Vector3 attackCenter = transform.position + orientedOffset;
        Vector3 halfExtents = attackBoxSize * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(attackCenter, halfExtents, Quaternion.identity, targetLayer);

        foreach (var col in hitColliders)
        {
            if (col.gameObject == gameObject)
                continue;

            if (col.TryGetComponent<Hittable>(out var hittable))
            {
                HitData hitData = originHitData;
                Vector3 dir = hitData.knockbackDirection;
                dir.x *= facingDirection;
                hitData.knockbackDirection = dir;

                hittable.Hit(hitData);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 orientedOffset = attackBoxOffset;
        orientedOffset.x *= facingDirection;

        Vector3 attackCenter = transform.position + orientedOffset;
        Gizmos.DrawWireCube(attackCenter, attackBoxSize);
    }
}