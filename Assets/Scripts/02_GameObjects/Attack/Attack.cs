
using UnityEngine;

public class Attack : ModuleFrame
{
    public PhysicsModule PhysicsModule { get; private set; }
    public RenderModule RenderModule { get; private set; }
    public ControlModule ControlModule { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other is Hittable)
        {
            Hittable hittable = other as Hittable;
            HitData data = new HitData();
            hittable.Hit(data);
        }
    }
}
