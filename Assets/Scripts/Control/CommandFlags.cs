
using System;

[Flags]
public enum CommandFlags
{
    None     = 0,
    Move     = 1 << 0,
    Jump     = 1 << 1,
    Attack   = 1 << 2
}
