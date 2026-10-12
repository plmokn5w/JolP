
using System;
using System.Collections.Generic;

[Serializable]
public class State
{
    private Dictionary<Type, StateData> datas = new();

    public void Init(List<StateData> dataList)
    {
        foreach (var data in dataList)
            datas.Add(data.GetType(), data);
    }

    public bool TryGetData<T>(out T data) where T : StateData
    {
        Type type = typeof(T);
        if (datas.ContainsKey(type))
        {
            data = datas[type] as T;
            return true;
        }
        data = default;
        return false;
    }
}
