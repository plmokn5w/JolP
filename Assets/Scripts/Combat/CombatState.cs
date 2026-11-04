
public enum CombatState
{
    Neutral = 0,
    LightFlinch = 1 << 1,
    HeavyHitstun = 1 << 2,
    Airborne = 1<< 3,
    Knockdown = 1<< 4,
    Wakeup = 1 << 5
}
