
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ModuleGroupAttribute : Attribute
{
    public Type GroupType { get; private set; }

    public ModuleGroupAttribute(Type groupType) { GroupType = groupType; }
}
