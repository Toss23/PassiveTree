public interface IPassivePresenter
{
    public IPassiveView PassiveView { get; }
    public Passive Passive { get; }

    public void Initialize();
    public void Enable();
    public void Disable();
}