
using System;
using System.Collections.Generic;

[Serializable]
public class CommandStateData : StateData
{
    [Serializable]
    public class Pair
    {
        public CommandFlags command;
        public StateKey state;
    }
    public List<Pair> pairs;
    public StateKey noInputState;
}
