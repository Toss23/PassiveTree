using System.Collections.Generic;

public class PassivePresenter : IPassivePresenter
{
    private static List<Passive> _passives;

    private IPassiveView _passiveView;
    private Passive _passive;

    public IPassiveView PassiveView { get { return _passiveView; } }
    public Passive Passive { get { return _passive; } }

    public PassivePresenter(IPassiveView passiveView, Passive passive)
    {
        _passiveView = passiveView;
        _passive = passive;

        if (_passives == null) _passives = new List<Passive>();
        if (_passives.Contains(_passive) == false) _passives.Add(_passive);
    }

    public void Initialize()
    {
        foreach (Passive passive in _passives)
        {
            if (passive != _passive)
            {
                foreach (string index in passive.IndexLinkedPassives)
                {
                    if (index == _passive.Index)
                    {
                        _passive.AddLinkedPassive(passive);
                        passive.AddLinkedPassive(_passive);
                        break;
                    }
                }
            }
        }
    }

    public void Enable()
    {
        _passiveView.SetDisplayName(_passive.DisplayName);
        _passiveView.BordersActive(false);
        _passive.OnLearned += OnPassiveLearned;
        _passive.OnForgotten += OnPassiveForgotten;
        _passive.OnSelect += OnSelectPassive;
        _passive.OnDeselect += OnDeselectPassive;
    }

    public void Disable()
    {
        _passive.OnLearned -= OnPassiveLearned;
        _passive.OnForgotten -= OnPassiveForgotten;
        _passive.OnSelect -= OnSelectPassive;
        _passive.OnDeselect -= OnDeselectPassive;
    }

    private void OnPassiveLearned()
    {
        _passiveView.SetIconColor(IconColor.OnLearnedColor);
    }

    private void OnPassiveForgotten()
    {
        _passiveView.SetIconColor(IconColor.OnNotLearnedColor);
    }

    private void OnSelectPassive()
    {
        _passiveView.BordersActive(true);
    }

    private void OnDeselectPassive()
    {
        _passiveView.BordersActive(false);
    }
}