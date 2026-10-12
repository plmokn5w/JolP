
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CommandStateData : StateData
{
    [field: SerializeField] public Dictionary<CommandFlags, StateKey> Map { get; private set; }
    [field: SerializeField] public StateKey noInputState { get; private set; }
}
