public class PassiveTreePresenter
{
    private PassiveTreeView _passiveTreeView;
    private PassiveTree _passiveTree;
    private PassivePresenter[] _passivePresenters;

    public PassiveTreePresenter(PassiveTree passiveTree, PassiveTreeView passiveTreeView, PassivePresenter[] passivePresenters)
    {
        _passiveTreeView = passiveTreeView;
        _passiveTree = passiveTree;
        _passivePresenters = passivePresenters;
    }

    public void Enable()
    {
        foreach (PassivePresenter passivePresenter in _passivePresenters)
        {
            passivePresenter.PassiveView.OnClick += () => OnClickPassive(passivePresenter.Passive);
        }

        _passiveTreeView.OnClickLearn += OnClickLearn;
        _passiveTreeView.OnClickForgot += OnClickForgot;
        _passiveTreeView.OnClickForgotAll += OnClickForgotAll;
    }

    public void Disable()
    {
        foreach (PassivePresenter passivePresenter in _passivePresenters)
        {
            passivePresenter.PassiveView.OnClick -= () => OnClickPassive(passivePresenter.Passive);
        }

        _passiveTreeView.OnClickLearn -= OnClickLearn;
        _passiveTreeView.OnClickForgot -= OnClickForgot;
        _passiveTreeView.OnClickForgotAll -= OnClickForgotAll;
    }

    private void OnClickPassive(Passive passive)
    {
        _passiveTree.SelectPassive(passive);
    }

    private void OnClickLearn()
    {
        _passiveTree.LearnPassive();
    }

    private void OnClickForgot()
    {
        _passiveTree.ForgotPassive();
    }

    private void OnClickForgotAll()
    {
        _passiveTree.ForgotAllPassives();
    }
}
