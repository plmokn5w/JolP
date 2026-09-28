
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string skillDescription;

    public float totalTime;

    [SerializeReference] public List<Hitbox> hitboxes;
    [SerializeReference] public List<SkillAction> actions;

    public AnimationCurve velocityX;
    public AnimationCurve velocityY;
    public AnimationCurve velocityZ;

    public Skill CreateSkill()
    {
        Skill skill = new Skill();
        skill.Initialize(this);
        return skill;
    }
}
