
[System.Flags]
public enum Command
{
    None = 0,
    Move = 1 << 0,
    Run = 1 << 1,
    Dash = 1 << 2,
    Jump = 1 << 3,
    Attack = 1 << 4,
    Skill = 1 << 5,
    Switch = 1 << 6,
}
