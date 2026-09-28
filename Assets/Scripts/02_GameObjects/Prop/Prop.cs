
public class Prop : ModuleFrame, Hittable
{
    public PhysicsModule PhysicsModule { get; private set; }
    public RenderModule RenderModule { get; private set; }
    public CombatModule CombatModule { get; private set; }
    public GaugeModule GaugeModule { get; private set; }

    public void Initialize(PropData data)
    {
        PhysicsModule = data.physicsModuleData.CreateModule() as PhysicsModule;
        RenderModule = data.renderModuleData.CreateModule() as RenderModule;
        CombatModule = data.combatModuleData.CreateModule() as CombatModule;
        GaugeModule = data.gaugeModuleData.CreateModule() as GaugeModule;
        Initialize();
    }

    private void Initialize()
    {
        PhysicsModule.Initialize(this);
        RenderModule.Initialize(this);
        CombatModule.Initialize(this);
        GaugeModule.Initialize(this);
    }

    public HitResult Hit(HitData data)
    {
        HitResult result = HitResult.Hit;
        return result;
    }

    private void Update()
    {
        CombatModule.OnUpdate();
    }

    private void FixedUpdate()
    {
        PhysicsModule.OnFixedUpdate();
    }
}
