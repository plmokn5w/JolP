
using R3;

public class Gauge
{
    public float CurValue { get; private set; }
    public float MaxValue { get; private set; }

    public Subject<Gauge> ValueChangeChannel { get; private set; }

    public Gauge(float value)
    {
        CurValue = MaxValue = value;
        ValueChangeChannel = new Subject<Gauge>();
    }

    public static implicit operator float(Gauge gauge) {  return gauge.CurValue; }

    public void Lose(float loss)
    {
        CurValue -= loss;
        ValueChangeChannel.OnNext(this);
    }

    public void Recover(float recovery)
    {
        CurValue += recovery;
        ValueChangeChannel.OnNext(this);
    }

    public void SetMaxValue(float maxValue)
    {
        MaxValue = maxValue;
    }
}
