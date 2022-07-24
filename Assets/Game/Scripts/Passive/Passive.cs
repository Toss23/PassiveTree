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

    // Здесь инициализируются веса, это нужно чтобы в дальнейшем понимать останиться ли свзять дерева с началом
    // при отмене изучения пассивки
    public void Initialize()
    {
        // Первый проход чтобы задать веса связанным пассивкам
        foreach (Passive passive in _linkedPassives)
        {
            if (_isBase)
                passive._weight = 1;
            else if (passive._weight == 0 || _weight < passive._weight - 1)
                passive._weight += _weight + 1;
        }

        // Второй проход чтобы связанные пассивки задали веса своим связанным пассивкам если это возможно
        foreach (Passive passive in _linkedPassives)
        {
            if (passive.IsBase == false)
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

    // Если связанные изученные пассивки имеют связи с пассивками меньшего веса, то можно забыть
    // Эта проверка позволяет понять разрушиться ли связь между остальными пассивками и началом дерева
    // Будет работать даже если базовых пассивок больше 1
    public bool CanBeForgotten()
    {
        foreach (Passive passive in _linkedPassives)
        {
            // Проверяем пассивку
            if (passive._isBase == false & passive._isLearned)
            {
                bool haveLinkWithBase = false;

                // Проверем есть ли у связанных изученных с ней пассивки связь с базовой
                foreach (Passive linkedPassive in passive._linkedPassives)
                {
                    if (linkedPassive != passive & linkedPassive._isLearned & linkedPassive._weight < passive._weight)
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