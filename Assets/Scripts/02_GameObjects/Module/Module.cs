
public abstract class Module
{
    public ModuleFrame Frame { get; protected set; }

    public virtual void Initialize(ModuleFrame frame)
    {
        Frame = frame;
        Frame.AddModule(this);
    }
}
