public enum ModifierType
{
    None, Fly, Jump, Run
}

public class Modifier
{
    private ModifierType _type;
    private int _value;

    public ModifierType Type { get { return _type; } }
    public int Value { get { return _value; } }

    public Modifier()
    {
        _type = ModifierType.None;
        _value = 0;
    }

    public Modifier(ModifierType type, int value)
    {
        _type = type;
        _value = value;
    }
}