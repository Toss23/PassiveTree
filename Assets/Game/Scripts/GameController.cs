using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PassiveTreeView _passiveTreeView;
    [SerializeField] private PassivePresenter[] _passivePresenters;

    private Character _character;
    private PassiveTree _passiveTree;
    private PassiveTreePresenter _passiveTreePresenter;

    private void Start()
    {
        foreach (PassivePresenter passivePresenter in _passivePresenters)
        {
            passivePresenter.Initialize();
            passivePresenter.Enable();
        }

        Passive[] passives = PassivePresenter.Passives.ToArray();

        // ������� ������ ��������� � ������
        _character = new Character();
        _passiveTree = new PassiveTree(passives, _character);
        _passiveTree.Initialize();

        // ������� ��������� ��� ����� ������ � �����������
        _passiveTreePresenter = new PassiveTreePresenter(_passiveTree, _passiveTreeView, _passivePresenters);
        _passiveTreePresenter.Enable();
    }
}