public class PassiveTreePresenter
{
    private PassiveTree _passiveTree;
    private IPassiveTreeView _passiveTreeView;
    private IPassivePresenter[] _passivePresenters;

    public PassiveTreePresenter(PassiveTree passiveTree, IPassiveTreeView passiveTreeView, IPassivePresenter[] passivePresenters)
    {
        _passiveTree = passiveTree;
        _passiveTreeView = passiveTreeView;
        _passivePresenters = passivePresenters;
    }

    public void Enable()
    {
        foreach (PassivePresenter passivePresenter in _passivePresenters)
        {
            passivePresenter.PassiveView.OnClick += () => OnClickPassive(passivePresenter.Passive);
        }

        ChangeSkillPointText(_passiveTree.SkillPoints);

        _passiveTreeView.OnClickLearn += OnClickLearn;
        _passiveTreeView.OnClickForgot += OnClickForgot;
        _passiveTreeView.OnClickForgotAll += OnClickForgotAll;
        _passiveTreeView.OnClickAddSkillPoint += AddSkillPassive;
        _passiveTree.OnSkillPointsChanged += ChangeSkillPointText;
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
        _passiveTreeView.OnClickAddSkillPoint -= AddSkillPassive;
        _passiveTree.OnSkillPointsChanged -= ChangeSkillPointText;
    }

    private void ChangeSkillPointText(int skillPoints)
    {
        _passiveTreeView.SetSkillPointText(skillPoints + " Skill Points");
    }

    private void AddSkillPassive()
    {
        _passiveTree.AddSkillPoint(1);
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
