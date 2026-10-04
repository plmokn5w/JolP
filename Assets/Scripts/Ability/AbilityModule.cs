
using UnityEngine;
using System.Collections.Generic;

public class AbilityModule : MonoBehaviour
{
    [SerializeReference] List<Ability> abilities;

    private void Awake()
    {
        foreach (var ability in abilities)
            ability.Init(gameObject);
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            if (ability.CanActivate())
                ability.Execute();
        }
    }
}
