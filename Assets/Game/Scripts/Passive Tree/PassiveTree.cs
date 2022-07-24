using System.Collections.Generic;
using UnityEngine;

public class PassiveTree
{
    private Passive[] _passives;
    private Character _character;
    private Passive _selectedPassive;
    private List<Passive> _passivesLearned;

    public PassiveTree(Passive[] passives, Character character)
    {
        _passives = passives;
        _character = character;
        _passivesLearned = new List<Passive>();
    }

    public void Initialize()
    {
        foreach (Passive passive in _passives)
        {
            passive.Initialize();
        }
    }

    public void SelectPassive(Passive passive)
    {
        _selectedPassive?.Deselect();
        _selectedPassive = passive;
        _selectedPassive.Select();
    }

    public void LearnPassive()
    {
        Debug.Log("Try Learn");
        if (_selectedPassive != null && _selectedPassive.CanBeLearned())
        {
            Debug.Log("Learn");
            if (_selectedPassive.IsBase)
            {
                _character.AddModifier(_selectedPassive.Modifier);
                _selectedPassive.Learn();
            }
            else if (_character.RemoveSkillPoint(_selectedPassive.PointCost))
            {
                _character.AddModifier(_selectedPassive.Modifier);
                _passivesLearned.Add(_selectedPassive);
                _selectedPassive.Learn();
            }
        }
    }

    public bool ForgotPassive()
    {
        if (_selectedPassive != null && _selectedPassive.IsLearned)
        {
            if (ForgotPassive(_selectedPassive))
            {
                _passivesLearned.Remove(_selectedPassive);
                return true;
            }
        }
        return false;
    }

    public void ForgotAllPassives()
    {
        foreach (Passive passive in _passivesLearned)
        {
            HardForgotPassive(passive);
        }
        _passivesLearned.Clear();
    }

    private bool ForgotPassive(Passive passive)
    {
        if (passive.CanBeForgotten())
        {
            passive.Forgot();
            _character.AddSkillPoint(passive.PointCost);
            _character.RemoveModifier(passive.Modifier);
            return true;
        }
        return false;
    }

    private void HardForgotPassive(Passive passive)
    {
        passive.Forgot();
        _character.AddSkillPoint(passive.PointCost);
        _character.RemoveModifier(passive.Modifier);
    }
}