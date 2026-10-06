
using UnityEngine;

public class CombatModule : MonoBehaviour
{
    [field: SerializeField] public float HitstunTimer = 0.0f;
    [Header("공격 판정 설정")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector3 attackBoxSize;
    [SerializeField] private Vector3 attackBoxOffset;
    [SerializeField] private HitData originHitData;

    private float facingDirection = 1f;

    public void SetFacingDirection(FacingSide facingSide)
    {
        //if (dir != 0) facingDirection = Mathf.Sign(dir);
        if (facingSide == FacingSide.Left)
            facingDirection = -1f;
        if (facingSide == FacingSide.Right)
            facingDirection = 1f;
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

    public void SetHitstun(float time)
    {
        HitstunTimer = time;
    }

    public void AddHitstun(float time)
    {
        HitstunTimer += time;
    }

    private void Update()
    {
        HitstunTimer = Mathf.MoveTowards(HitstunTimer, 0f, Time.deltaTime);
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