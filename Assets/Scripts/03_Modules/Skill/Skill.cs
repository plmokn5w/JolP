
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public class Skill
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private float totalTime;

    private List<Hitbox> hitboxes;
    private int curHitboxIndex;

    private List<ModuleFrame> targets;
    private List<SkillAction> actions;

    private AnimationCurve velocityX;
    private AnimationCurve velocityY;
    private AnimationCurve velocityZ;

    private Transform transform;
    private PhysicsModule physicsModule;
    private ControlModule controlModule;

    public void Initialize(SkillData data)
    {
        Name = data.skillName;
        Description = data.skillDescription;
        hitboxes = new List<Hitbox>();
        for (int i = 0; i < data.hitboxes.Count; i++)
            hitboxes.Add(data.hitboxes[i].Clone());
        curHitboxIndex = 0;
        targets = new List<ModuleFrame>();
        actions = new List<SkillAction>();
        for (int i = 0; i < data.actions.Count; i++)
            actions.Add(data.actions[i].Clone());
        totalTime = data.totalTime;
        velocityX = data.velocityX;
        velocityY = data.velocityY;
        velocityZ = data.velocityZ;
    }

    public void Initialize(ModuleFrame frame)
    {
        transform = frame.transform;
        physicsModule = frame.GetModule<PhysicsModule>();
        controlModule = frame.GetModule<ControlModule>();
        for (int i = 0; i < actions.Count; i++)
            actions[i].Initialize(frame);
    }

    public async UniTask Execute(CancellationToken token)
    {
        float timer = 0f;
        physicsModule.SetGravity(0f);
        physicsModule.ResetVelocity();
        float dirX = controlModule.FacingDirection == FacingDirection.Left ? -1f : 1f;

        while (timer <= totalTime)
        {
            float normalizedTime = timer / totalTime;

            if (curHitboxIndex < hitboxes.Count && normalizedTime >= hitboxes[curHitboxIndex].Time)
            {
                targets.Clear();
                hitboxes[curHitboxIndex].ScanTargets(transform.position, controlModule.FacingDirection, targets);
                for (int i = 0; i < actions.Count; i++)
                    actions[i].Action(targets);
                curHitboxIndex++;
            }

            Vector2 horizontal = new Vector2(velocityX.Evaluate(normalizedTime) * dirX, velocityZ.Evaluate(normalizedTime));
            float vertical = velocityY.Evaluate(normalizedTime);
            physicsModule.Dash(horizontal, horizontal.magnitude);
            physicsModule.Launch(vertical);

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            timer += Time.deltaTime;
        }
    }
}
