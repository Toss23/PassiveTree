using System;
using System.Collections.Generic;

public class Passive
{
    public event Action OnLearned;
    public event Action OnForgotten;
    public event Action OnSelect;
    public event Action OnDeselect;

    private string _displayName;
    private string _index;
    private bool _isBase;
    private bool _isLearned;
    private int _pointCost;
    private Modifier _modifier;
    private List<Passive> _linkedPassives;
    private string[] _indexLinkedPassives;

    public string DisplayName { get { return _displayName; } }
    public string Index { get { return _index; } }
    public bool IsBase { get { return _isBase; } }
    public bool IsLearned { get { return _isLearned; } }
    public int PointCost { get { return _pointCost; } }
    public Modifier Modifier { get { return _modifier; } }
    public string[] IndexLinkedPassives { get { return _indexLinkedPassives; } }

    public Passive(IPassiveData passiveData)
    {
        _displayName = passiveData.DisplayName;
        _index = passiveData.Index;
        _isBase = passiveData.IsBase;
        _isLearned = passiveData.IsBase;
        _pointCost = _isBase ? 0 : passiveData.PointCost;
        _modifier = passiveData.Modifier;
        _linkedPassives = new List<Passive>();
        _indexLinkedPassives = passiveData.IndexLinkedPassives;
    }

    public void AddLinkedPassive(Passive passive)
    {
        if (_linkedPassives.Contains(passive) == false)
            _linkedPassives.Add(passive);
    }

    public void Select()
    {
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        OnDeselect?.Invoke();
    }

    public void Learn()
    {
        _isLearned = true;
        OnLearned?.Invoke();
    }

    public void Forgot()
    {
        _isLearned = false;
        OnForgotten?.Invoke();
    }

    public bool CanBeLearned(int haveSkillPoints)
    {
        if (_isBase)
            return false;

        if (_isLearned == false & haveSkillPoints >= _pointCost)
        {
            foreach (Passive passive in _linkedPassives)
            {
                if (passive.IsBase | passive.IsLearned)
                    return true;
            }
        }
        return false;
    }

    public bool CanBeForgotten()
    {
        if (_isLearned == false | _isBase)
            return false;

        foreach (Passive passive in _linkedPassives)
        {
            if (passive._isBase == false && passive._isLearned)
            {
                List<Passive> previousPassives = new List<Passive>();
                previousPassives.Add(this);

                bool havePathToBase = HaveLinkWithBase(passive, previousPassives);
                if (havePathToBase == false)
                    return false;
            }
        }
        return true;
    }

    private bool HaveLinkWithBase(Passive passive, List<Passive> previousPassives)
    {
        foreach (Passive linkedPassive in passive._linkedPassives)
        {
            if (linkedPassive._isBase)
                return true;

            if (linkedPassive._isLearned && previousPassives.Contains(linkedPassive) == false)
            {
                previousPassives.Add(passive);
                if (HaveLinkWithBase(linkedPassive, previousPassives))
                    return true;
            }
        }
        return false;
    }
}