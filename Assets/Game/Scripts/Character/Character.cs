using System.Collections.Generic;

public class Character
{
    private List<Modifier> _modifiers;

    public Character()
    {
        _modifiers = new List<Modifier>();
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