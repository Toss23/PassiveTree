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

        _passiveTreeView.OnClickLearn += LearnSelectedPassive;
        _passiveTreeView.OnClickForgot += ForgotSelectedPassive;
        _passiveTreeView.OnClickForgotAll += ForgotAllPassives;
        _passiveTreeView.OnClickAddSkillPoint += AddSkillPassive;
        _passiveTree.OnSkillPointsChanged += ChangeSkillPointsText;
        _passiveTree.OnSelectedPassiveChanged += ChangeSkillPointsCostText;

        _passiveTreeView.LearnPassiveButtonEnable(false);
        _passiveTreeView.ForgotPassiveButtonEnable(false);

        ChangeSkillPointsText(_passiveTree.SkillPoints);
        _passiveTree.DeselectPassive();
    }

    public void Disable()
    {
        foreach (PassivePresenter passivePresenter in _passivePresenters)
        {
            passivePresenter.PassiveView.OnClick -= () => OnClickPassive(passivePresenter.Passive);
        }

        _passiveTreeView.OnClickLearn -= LearnSelectedPassive;
        _passiveTreeView.OnClickForgot -= ForgotSelectedPassive;
        _passiveTreeView.OnClickForgotAll -= ForgotAllPassives;
        _passiveTreeView.OnClickAddSkillPoint -= AddSkillPassive;
        _passiveTree.OnSkillPointsChanged -= ChangeSkillPointsText;
        _passiveTree.OnSelectedPassiveChanged -= ChangeSkillPointsCostText;
    }

    private void ChangeSkillPointsText(int skillPoints)
    {
        _passiveTreeView.SetSkillPointsText(skillPoints + " SP");
    }

    private void ChangeSkillPointsCostText(Passive passive)
    {
        if (passive != null)
            _passiveTreeView.SetSkillPointsCostText("Require " + passive.PointCost + " SP");
        else
            _passiveTreeView.SetSkillPointsCostText("");
    }

    private void AddSkillPassive()
    {
        _passiveTree.AddSkillPoint(1);
        UpdatePassiveTreeViewButtons(_passiveTree.SelectedPassive);
    }

    private void OnClickPassive(Passive passive)
    {
        _passiveTree.SelectPassive(passive);
        UpdatePassiveTreeViewButtons(passive);
    }

    private void LearnSelectedPassive()
    {
        _passiveTree.LearnPassive();
        UpdatePassiveTreeViewButtons(_passiveTree.SelectedPassive);
    }

    private void ForgotSelectedPassive()
    {
        _passiveTree.ForgotPassive();
        UpdatePassiveTreeViewButtons(_passiveTree.SelectedPassive);
    }

    private void ForgotAllPassives()
    {
        _passiveTree.ForgotAllPassives();
        UpdatePassiveTreeViewButtons(_passiveTree.SelectedPassive);
    }

    private void UpdatePassiveTreeViewButtons(Passive passive)
    {
        if (passive != null)
        {
            _passiveTreeView.LearnPassiveButtonEnable(passive.CanBeLearned(_passiveTree.SkillPoints));
            _passiveTreeView.ForgotPassiveButtonEnable(passive.CanBeForgotten());
        }
    }
}