
using UnityEngine;

public class Scarecrow : Entity, Kinetic, Bounceable
{
    private PhysicsModule physicsModule;
    private GaugeModule gaugeModule;

    private void Awake()
    {
        physicsModule = GetComponent<PhysicsModule>();
        gaugeModule = GetComponent<GaugeModule>();
    }

    public void Teleport(Vector3 position) { physicsModule.Teleport(position); }

    public void Knockback(Vector3 dir, float force)
    {
        Vector2 horizontal = new Vector2(dir.x, dir.z);
        float vertical = dir.y;
        physicsModule.SetImpulseForce(horizontal, force);
        if (vertical > 0f)
            physicsModule.Launch(force);
    }

    public void GetPulled(Vector3 position, float force)
    {
        Vector3 dir = position - transform.position;
        Vector2 horizontal = new Vector2(dir.x, dir.z);
        float vertical = dir.y;
        physicsModule.AddContinousForce(horizontal, force);
        if (vertical > 0f)
            physicsModule.Launch(force);
    }

    public void Hit(HitData hitData)
    {
        Knockback(hitData.knockbackDirection, hitData.knockbackForce);
        gaugeModule[GaugeType.Health].Lose(hitData.damage);
    }

    public void ApplyBounce(HitData hitData)
    {
        Hit(hitData);
    }
}
