using System.Collections.Generic;

public class Character
{
    private int _skillPoints = 100;
    private List<Modifier> _modifiers;

    public int SkillPoints { get { return _skillPoints; } }

    public Character()
    {
        _modifiers = new List<Modifier>();
    }

    public void AddSkillPoint(int value)
    {
        _skillPoints += value;
    }

    public bool RemoveSkillPoint(int value)
    {
        if (_skillPoints - value >= 0)
        {
            _skillPoints -= value;
            return true;
        }
        return false;
    }

    public void AddModifier(Modifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public bool RemoveModifier(Modifier modifier)
    {
        return _modifiers.Remove(modifier);
    }
}