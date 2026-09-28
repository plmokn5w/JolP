
using System.Collections.Generic;

public class BuffModule : Module
{
    private List<Buff> buffs;

    public void Initialize(BuffModuleData data)
    {
        buffs = new List<Buff>();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < buffs.Count; i++)
        {

        }
    }
}
