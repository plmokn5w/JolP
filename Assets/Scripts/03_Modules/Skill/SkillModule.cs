
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

public class SkillModule : Module
{
    private Dictionary<SkillKey, Skill> skills;

    private CancellationTokenSource cts;

    public void Initialize(SkillModuleData data)
    {
        skills = new Dictionary<SkillKey, Skill>();
        for (int i = 0; i < data.skills.Count; i++)
            skills.Add(data.skills[i].key, data.skills[i].data.CreateSkill());
    }

    public override void Initialize(ModuleFrame frame)
    {
        base.Initialize(frame);
        foreach (var skill in skills.Values)
            skill.Initialize(frame);
    }

    public void ExecuteSkill(SkillKey key)
    {
        Skill skill = skills[key];
        cts = new CancellationTokenSource();
        skill.Execute(cts.Token).Forget();
    }
}
