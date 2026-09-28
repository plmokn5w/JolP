
using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ModuleFrame : MonoBehaviour
{
    private Dictionary<Type, Module> modules = new Dictionary<Type, Module>();

    public T GetModule<T>() where T : Module
    {
        if (modules.TryGetValue(typeof(T), out var module))
            return module as T;
        return null;
    }

    public void AddModule<T>(T module) where T : Module
    {
        var type = module.GetType();

        var attribute = Attribute.GetCustomAttribute(type, typeof(ModuleGroupAttribute)) as ModuleGroupAttribute;
        if (attribute != null)
            type = attribute.GroupType;

        if (!modules.ContainsKey(type))
            modules.Add(type, module);
    }
}
