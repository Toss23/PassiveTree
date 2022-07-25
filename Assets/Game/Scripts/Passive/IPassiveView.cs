using System;

public enum IconColor
{
    OnLearnedColor, OnNotLearnedColor
}

public interface IPassiveView
{
    public event Action OnClick;

    public PassivePresenter PassivePresenter { get; }

    public void SetDisplayName(string displayName);
    public void SetIconColor(IconColor color);
    public void BordersActive(bool active);
}