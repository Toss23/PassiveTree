using System;
using System.Collections.Generic;

public class PassiveTree
{
    public event Action<int> OnSkillPointsChanged;
    public event Action<Passive> OnSelectedPassiveChanged;

    private Character _character;
    private Passive _selectedPassive;
    private List<Passive> _passivesLearned;
    private int _skillPoints;

    public Passive SelectedPassive { get { return _selectedPassive; } }
    public int SkillPoints { get { return _skillPoints; } }

    public PassiveTree(Character character)
    {
        _character = character;
        _passivesLearned = new List<Passive>();
    }

    public void SelectPassive(Passive passive)
    {
        _selectedPassive?.Deselect();
        _selectedPassive = passive;
        _selectedPassive.Select();
        OnSelectedPassiveChanged?.Invoke(_selectedPassive);
    }

    public void DeselectPassive()
    {
        _selectedPassive?.Deselect();
        _selectedPassive = null;
        OnSelectedPassiveChanged?.Invoke(_selectedPassive);
    }

    public void LearnPassive()
    {
        if (_selectedPassive != null && _selectedPassive.CanBeLearned(_skillPoints))
        {
            if (RemoveSkillPoint(_selectedPassive.PointCost))
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