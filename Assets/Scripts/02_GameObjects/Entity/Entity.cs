
using R3;
using UnityEngine;

public abstract class Entity : ModuleFrame, Kinetic, Hittable, Damageable
{
    public PhysicsModule PhysicsModule { get; private set; }
    public RenderModule RenderModule { get; private set; }
    public CombatModule CombatModule { get; private set; }
    public ControlModule ControlModule { get; private set; }
    public StatModule StatModule { get; private set; }
    public GaugeModule GaugeModule { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public SkillModule SkillModule { get; private set; }
    public BuffModule BuffModule { get; private set; }

    public void Initialize(EntityData data)
    {
        PhysicsModule = data.physicsModuleData.CreateModule() as PhysicsModule;
        RenderModule = data.renderModuleData.CreateModule() as RenderModule;
        CombatModule = data.combatModuleData.CreateModule() as CombatModule;
        ControlModule = data.controlModuleData.CreateModule() as ControlModule;
        StatModule = data.statModuleData.CreateModule() as StatModule;
        GaugeModule = data.gaugeModuleData.CreateModule() as GaugeModule;
        StateMachine = data.stateMachineData.CreateModule() as StateMachine;
        SkillModule = data.skillModuleData.CreateModule() as SkillModule;
        BuffModule = data.buffModuleData.CreateModule() as BuffModule;
        Initialize();
    }

    private void Initialize()
    {
        //게임엔진의 필수인 물리와 그래픽입니다
        PhysicsModule.Initialize(this);
        RenderModule.Initialize(this);

        //게임에 필수인 전투입니다
        CombatModule.Initialize(this);

        //컨트롤입니다
        ControlModule.Initialize(this);
        Debug.Log(GetModule<ControlModule>());

        //데이터입니다
        StatModule.Initialize(this);
        GaugeModule.Initialize(this);

        SkillModule.Initialize(this);

        BuffModule.Initialize(this);

        // 상태액션땜에 맨마지막에 해줘야됨
        StateMachine.Initialize(this);

        // 이벤트 구독 연결 예시
        // 임시고 이걸 for 반복문으로 바꾸면 됩니다 ^^
        // (int)StatType == (int)GaugeType 대응하는 것들끼리 연결
        Stat healthStat = StatModule[StatType.Health];
        Gauge healthGauge = GaugeModule[GaugeType.Health];
        healthStat.ValueChangeChannel.Subscribe(value => { healthGauge.SetMaxValue(value); });
        healthGauge.SetMaxValue(healthStat);

        // 상태머신 <- 렌더링 연결
        StateMachine.StateChangeChannel.Subscribe(key => { RenderModule.RenderState(key); });
    }

    public void Teleport(Vector3 position) { PhysicsModule.Teleport(position); }

    public void Knockback(Vector3 dir, float force)
    {
        dir.Normalize();
        Vector2 horizontal = new Vector2(dir.x, dir.z);
        PhysicsModule.ResetVelocity();
        PhysicsModule.AddImpulseForce(horizontal, force);
        PhysicsModule.Launch(dir.y * force);
    }

    public void Grab(Vector3 position, float force)
    {
        Vector3 dir = position - transform.position;
        PhysicsModule.AddContinousForce(dir, force);
    }

    public HitResult Hit(HitData data)
    {
        HitResult result = HitResult.Hit;

        // 타격판정 바꾸는 코드

        if (result == HitResult.Hit)
        {
            Knockback(data.knockbackDirection, data.knockbackForce);
            TakeDamage(data.damageData);
            //StateMachine.TryChangeState(data.nextState);
        }
        return HitResult.Hit;
    }

    public void TakeDamage(DamageData data)
    {
        GaugeModule[GaugeType.Health].Lose(data.damage);
    }

    private void Update()
    {
        ControlModule.OnUpdate();
        StateMachine.OnUpdate();
        CombatModule.OnUpdate();
        BuffModule.OnUpdate();
    }

    private void FixedUpdate()
    {
        PhysicsModule.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        RenderModule.OnLateUpdate();
    }
}
