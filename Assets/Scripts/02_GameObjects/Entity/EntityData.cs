
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/GameObjects/EntityData")]
public class EntityData : ScriptableObject
{
    [Header("프리팹")]
    public GameObject prefab;

    [Header("물리")]
    public PhysicsModuleData physicsModuleData;

    [Header("애니메이션")]
    public RenderModuleData renderModuleData;

    [Header("전투")]
    public CombatModuleData combatModuleData;

    [Header("컨트롤")]
    public ControlModuleData controlModuleData;

    [Header("스탯")]
    public StatModuleData statModuleData;

    [Header("게이지")]
    public GaugeModuleData gaugeModuleData;

    [Header("상태머신")]
    public StateMachineData stateMachineData;

    [Header("스킬")]
    public SkillModuleData skillModuleData;

    [Header("버프")]
    public BuffModuleData buffModuleData;
}
