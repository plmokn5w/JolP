
using UnityEngine;

[CreateAssetMenu(fileName = "PropData", menuName = "Scriptable Objects/GameObjects/PropData")]
public class PropData : ScriptableObject
{
    [Header("프리팹")]
    public GameObject prefab;

    [Header("물리")]
    public PhysicsModuleData physicsModuleData;

    [Header("애니메이션")]
    public RenderModuleData renderModuleData;

    [Header("전투")]
    public CombatModuleData combatModuleData;

    [Header("게이지")]
    public GaugeModuleData gaugeModuleData;
}
