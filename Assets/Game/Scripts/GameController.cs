using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PassiveTreeView _passiveTreeView;
    [SerializeField] private PassiveView[] _passiveViews;

    private Character _character;
    private PassiveTree _passiveTree;
    private PassiveTreePresenter _passiveTreePresenter;

    // ������ ��� ������������� MVP view ������� presenter-�, �� � ������ ������ � ����� �� ��������� ��� PassiveTreeView
    // � ������� ��� ����, ������� �������� presenter-� �� view
    private void Start()
    {
        PassivePresenter[] passivePresenters = new PassivePresenter[_passiveViews.Length];
        for (int i = 0; i < passivePresenters.Length; i++)
        {
            passivePresenters[i] = _passiveViews[i].PassivePresenter;
        }

        Passive[] passives = PassivePresenter.Passives.ToArray();

        // ������� ������ ��������� � ������
        _character = new Character();
        _passiveTree = new PassiveTree(passives, _character);

        // ������� ��������� ��� ����� ������ � �����������
        _passiveTreePresenter = new PassiveTreePresenter(_passiveTree, _passiveTreeView, passivePresenters);
        _passiveTreePresenter.Enable();
    }
}