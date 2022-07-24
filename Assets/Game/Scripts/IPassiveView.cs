using System;

public interface IPassiveView
{
    public event Action OnClick;

    public void SetDisplayName(string displayName);
    public void SetIconState(bool learned);
    public void BordersActive(bool active);
}