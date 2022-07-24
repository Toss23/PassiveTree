using System;

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
    private int _weight;
    private bool _initialized;

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
        _weight = 0;
    }

    // ����� ���������������� ����, ��� ����� ����� � ���������� �������� ���������� �� ������ ������ � �������
    // ��� ������ �������� ��������
    public void Initialize()
    {
        _initialized = true;

        // ������ ������ ����� ������ ���� ��������� ���������
        foreach (Passive passive in _linkedPassives)
        {
            if (passive.IsBase == false & passive._weight == 0)
            {
                if (_isBase)
                    passive._weight = 1;
                else
                    passive._weight += _weight + 1;
            }
        }

        // ������ ������ ����� ��������� �������� ������ ���� ����� ��������� ��������� ���� ��� ��������
        foreach (Passive passive in _linkedPassives)
        {
            if (passive.IsBase == false & passive._initialized == false)
                passive.Initialize();
        }
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

    public bool CanBeLearned()
    {
        if (_isLearned == false)
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

    // ���� ��������� ��������� �������� ����� ����� � ���������� �������� ����, �� ����� ������
    // ��� �������� ��������� ������ ����������� �� ����� ����� ���������� ���������� � ������� ������
    public bool CanBeForgotten()
    {
        foreach (Passive passive in _linkedPassives)
        {
            if (passive._isLearned & passive._weight > _weight)
            {
                bool haveLinkWithBase = false;

                foreach (Passive linkedPassive in passive._linkedPassives)
                {
                    if (linkedPassive != this & linkedPassive._isLearned & linkedPassive._weight < passive._weight)
                    {
                        haveLinkWithBase = true;
                        break;
                    }
                }

                if (haveLinkWithBase == false)
                    return false;
            }
        }
        return true;
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