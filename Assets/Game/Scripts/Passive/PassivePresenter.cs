using System.Collections.Generic;

public class PassivePresenter : IPassivePresenter
{
    public static List<Passive> Passives { get; private set; }

    private IPassiveView _passiveView;
    private Passive _passive;

    public IPassiveView PassiveView { get { return _passiveView; } }
    public Passive Passive { get { return _passive; } }

    public PassivePresenter(IPassiveView passiveView, Passive passive)
    {
        _passiveView = passiveView;
        _passive = passive;

        if (Passives == null) Passives = new List<Passive>();
        if (Passives.Contains(_passive) == false) Passives.Add(_passive);
    }

    public void Initialize()
    {
        foreach (Passive passive in Passives)
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