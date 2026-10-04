
using UnityEngine;

public interface Kinetic
{
    public void Teleport(Vector3 position);

    public void Knockback(Vector3 dir, float force);

    public void GetPulled(Vector3 position, float force);
}