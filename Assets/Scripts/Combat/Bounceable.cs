
using UnityEngine;

public interface Bounceable : Hittable
{
    public void ApplyBounce(HitData hitData);
}
