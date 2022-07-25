using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Data", menuName = "Passive Tree/Create Passive")]
public class PassiveData : ScriptableObject, IPassiveData
{
    [SerializeField] private string _index;
    [SerializeField] private string _displayName;
    [Space(10)]
    [SerializeField] private bool _isBase;
    [SerializeField] private int _pointCost;
    [Space(10)]
    [SerializeField] private ModifierType _modifierType;
    [SerializeField] private int _modifierValue;
    [Space(10)]
    [SerializeField] private string[] _indexLinkedPassives;

    private Modifier _modifier;

    public string Index { get { return _index; } }
    public string DisplayName { get { return _displayName; } }
    public bool IsBase { get { return _isBase; } }
    public int PointCost { get { return _pointCost; } }
    public Modifier Modifier 
    { 
        get
        {
            if (_modifier == null)
                _modifier = new Modifier(_modifierType, _modifierValue);

            return _modifier;
        } 
    }
    public string[] IndexLinkedPassives { get { return _indexLinkedPassives; } }
}