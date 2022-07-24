using System.Collections.Generic;
using UnityEngine;

public class PassivePresenter : MonoBehaviour
{
    public static List<Passive> Passives { get; private set; }

    [SerializeField] private IPassiveView _passiveView;
    [SerializeField] private PassiveData _passiveData;

    private Passive _passive;

    public Passive Passive { get { return _passive; } }
    public IPassiveView PassiveView { get { return _passiveView; } }

    private void Awake()
    {
        _passive = new Passive(_passiveData.Index, _passiveData.IsBase, _passiveData.PointCost, _passiveData.Modifier);

        if (Passives == null) Passives = new List<Passive>();
        Passives.Add(_passive);
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
    }

    public void Enable()
    {
        _passiveView.SetDisplayName(_passiveData.DisplayName);
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
        _passiveView.SetIconState(true);
    }

    private void OnPassiveForgotten()
    {
        _passiveView.SetIconState(false);
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