using System;
using System.Collections.Generic;

public class PassiveTree
{
    public event Action<int> OnSkillPointsChanged;

    private Passive[] _passives;
    private Character _character;
    private Passive _selectedPassive;
    private List<Passive> _passivesLearned;
    private int _skillPoints;

    public int SkillPoints { get { return _skillPoints; } }

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
            if (passive.IsBase)
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
        if (_selectedPassive != null && _selectedPassive.CanBeLearned())
        {
            if (_selectedPassive.IsBase)
            {
                _character.AddModifier(_selectedPassive.Modifier);
                _selectedPassive.Learn();
            }
            else if (RemoveSkillPoint(_selectedPassive.PointCost))
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
            if (_selectedPassive.CanBeForgotten())
            {
                _selectedPassive.Forgot();
                AddSkillPoint(_selectedPassive.PointCost);
                _character.RemoveModifier(_selectedPassive.Modifier);
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

    public void AddSkillPoint(int value)
    {
        _skillPoints += value;
        OnSkillPointsChanged?.Invoke(_skillPoints);
    }

    public bool RemoveSkillPoint(int value)
    {
        if (_skillPoints - value >= 0)
        {
            _skillPoints -= value;
            OnSkillPointsChanged?.Invoke(_skillPoints);
            return true;
        }
        return false;
    }

    private void HardForgotPassive(Passive passive)
    {
        passive.Forgot();
        AddSkillPoint(passive.PointCost);
        _character.RemoveModifier(passive.Modifier);
    }
}