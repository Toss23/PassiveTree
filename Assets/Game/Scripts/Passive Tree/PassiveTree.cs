using System.Collections.Generic;

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
            if (_selectedPassive.CanBeForgotten())
            {
                _selectedPassive.Forgot();
                _character.AddSkillPoint(_selectedPassive.PointCost);
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

    private void HardForgotPassive(Passive passive)
    {
        passive.Forgot();
        _character.AddSkillPoint(passive.PointCost);
        _character.RemoveModifier(passive.Modifier);
    }
}