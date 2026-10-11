
using System;
using UnityEngine;

[Serializable]
public class AttackAbility : Ability
{
    [Header("공격 판정 설정")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector3 attackBoxSize;
    [SerializeField] private Vector3 attackBoxOffset;
    [SerializeField] private HitData originHitData;

    private Transform transform;
    private ControlModule controlModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        transform = gameObject.transform;
        controlModule = gameObject.GetComponent<ControlModule>();
    }

    public override bool CanActivate()
    {
        return controlModule.Flags.HasFlag(CommandFlags.Attack);
    }

    public override void Execute()
    {
        Vector3 orientedOffset = attackBoxOffset;
        orientedOffset.x *= (float)controlModule.LookDirection.Value;

        Vector3 attackCenter = transform.position + orientedOffset;
        Vector3 halfExtents = attackBoxSize * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(attackCenter, halfExtents, Quaternion.identity, targetLayer);

        foreach (var col in hitColliders)
        {
            if (col.gameObject == frame)
                continue;

            if (col.TryGetComponent<Hittable>(out var hittable))
            {
                HitData hitData = originHitData;
                Vector3 dir = hitData.knockbackDirection;
                dir.x *= (float)controlModule.LookDirection.Value;
                hitData.knockbackDirection = dir;

                hittable.Hit(hitData);
            }
        }
    }
}
