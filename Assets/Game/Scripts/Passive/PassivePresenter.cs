using System.Collections.Generic;
using UnityEngine;

public class PassivePresenter : MonoBehaviour, IPassivePresenter
{
    public static List<Passive> Passives { get; private set; }

    [SerializeField] private PassiveView _passiveViewLink;
    [SerializeField] private PassiveData _passiveData;

    private IPassiveView _passiveView;
    private Passive _passive;

    public IPassiveView PassiveView { get { return _passiveView; } }
    public Passive Passive { get { return _passive; } }

    private void Awake()
    {
        _passiveView = _passiveViewLink;
        _passive = new Passive(_passiveData.Index, _passiveData.IsBase, _passiveData.PointCost, _passiveData.Modifier);

        if (Passives == null) Passives = new List<Passive>();
        if (Passives.Contains(_passive) == false) Passives.Add(_passive);
    }

    public void Initialize()
    {
        List<Passive> linkedPassives = new List<Passive>();

        foreach (string index in _passiveData.IndexLinkedPassives)
        {
            foreach(Passive passive in Passives)
            {
                if (index == passive.Index)
                {
                    linkedPassives.Add(passive);
                    break;
                }
            }
        }

        _passive.LinkedPassives = linkedPassives.ToArray();
        if (_passive.IsBase) _passive.Learn();
    }

    public void Enable()
    {
        _passiveView.SetDisplayName(_passiveData.DisplayName);
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