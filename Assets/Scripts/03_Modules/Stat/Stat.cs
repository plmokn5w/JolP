
using R3;

public class Stat
{
    public float Value { get; private set; }

    public Subject<Stat> ValueChangeChannel { get; private set; }

    public Stat(float value)
    {
        Value = value;
        ValueChangeChannel = new Subject<Stat>();
    }

    public static implicit operator float(Stat stat) { return stat.Value; }

    public void SetValue(float value)
    {
        Value = value;
        ValueChangeChannel.OnNext(this);
    }
}
