
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CubeHitbox : Hitbox
{
    [field: SerializeField] private Vector3 halfExtents;
    private RaycastHit[] raycastHits = new RaycastHit[32];

    public override void ScanTargets(Vector3 origin, FacingDirection facingDirection, List<ModuleFrame> targets)
    {
        Vector3 direction = facingDirection == FacingDirection.Left ? Vector3.left : Vector3.up;
        Physics.BoxCastNonAlloc(center: origin + Offset, halfExtents: halfExtents, direction: direction, raycastHits, orientation: Quaternion.identity, maxDistance: 0f, layerMask: LayerMask.NameToLayer("Entity"), queryTriggerInteraction: QueryTriggerInteraction.Ignore);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            Collider collider = raycastHits[i].collider;
            if (collider.TryGetComponent<ModuleFrame>(out var target))
                targets.Add(target);
        }
    }
}
