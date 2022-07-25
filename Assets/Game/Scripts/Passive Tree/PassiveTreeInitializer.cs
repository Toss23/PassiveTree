using UnityEngine;

public class PassiveTreeInitializer : MonoBehaviour
{
    [SerializeField] private PassiveTreeView _passiveTreeView;
    [SerializeField] private PassiveView[] _passiveViews;

    private Character _character;
    private PassiveTree _passiveTree;
    private PassiveTreePresenter _passiveTreePresenter;

    // Обычно, при использовании MVP, View создает Presenter-а, но в данном случае я решил не усложнять код PassiveTreeView
    private void Start()
    {
        _character = new Character();
        _passiveTree = new PassiveTree(_character);

        _passiveTreePresenter = new PassiveTreePresenter(_passiveTree, _passiveTreeView, _passiveViews);
        _passiveTreePresenter.Enable();
    }
}