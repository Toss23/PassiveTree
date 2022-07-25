using System;
using System.Collections.Generic;
using UnityEngine;

public class Passive
{
    public event Action OnLearned;
    public event Action OnForgotten;
    public event Action OnSelect;
    public event Action OnDeselect;

    private string _index;
    private bool _isBase;
    private bool _isLearned;
    private int _pointCost;
    private Modifier _modifier;
    private Passive[] _linkedPassives;

    public string Index { get { return _index; } }
    public bool IsBase { get { return _isBase; } }
    public bool IsLearned { get { return _isLearned; } }
    public int PointCost { get { return _pointCost; } }
    public Modifier Modifier { get { return _modifier; } }
    public Passive[] LinkedPassives { set { _linkedPassives = value; } }

    public Passive(string index, bool isBase, int pointCost, Modifier modifier)
    {
        _index = index;
        _isBase = isBase;
        _isLearned = isBase;
        _pointCost = isBase ? 0 : pointCost;
        _modifier = modifier;
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
        if (_isLearned == false & haveSkillPoints >= _pointCost)
        {
            if (_isBase)
            {
                return true;
            }
            else
            {
                foreach (Passive passive in _linkedPassives)
                {
                    if (passive.IsBase | passive.IsLearned)
                        return true;
                }
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
                Debug.Log("Try: " + passive._index);
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

            foreach (Passive previousPassive in previousPassives)
            {
                if (linkedPassive != previousPassive & linkedPassive._isLearned)
                {
                    previousPassives.Add(passive);
                    if (HaveLinkWithBase(linkedPassive, previousPassives))
                        return true;
                }
            }
        }
        return false;
    }

    public void Select()
    {
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        OnDeselect?.Invoke();
    }
}