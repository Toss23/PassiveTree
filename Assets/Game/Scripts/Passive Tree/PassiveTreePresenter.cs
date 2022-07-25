public class PassiveTreePresenter
{
    private PassiveTree _passiveTree;
    private IPassiveTreeView _passiveTreeView;
    private IPassiveView[] _passiveViews;

    public PassiveTreePresenter(PassiveTree passiveTree, IPassiveTreeView passiveTreeView, IPassiveView[] passiveViews)
    {
        _passiveTree = passiveTree;
        _passiveTreeView = passiveTreeView;
        _passiveViews = passiveViews;
    }

    public void Enable()
    {
        foreach (IPassiveView passiveView in _passiveViews)
        {
            passiveView.OnClick += () => SelectPassive(passiveView.PassivePresenter.Passive);
        }

        _passiveTreeView.OnClickLearn += LearnSelectedPassive;
        _passiveTreeView.OnClickForgot += ForgotSelectedPassive;
        _passiveTreeView.OnClickForgotAll += ForgotAllPassives;
        _passiveTreeView.OnClickAddSkillPoint += AddSkillPoint;
        _passiveTree.OnSkillPointsChanged += ChangeSkillPointsText;
        _passiveTree.OnSelectedPassiveChanged += ChangeSkillPointsCostText;

        _passiveTreeView.LearnPassiveButtonEnable(false);
        _passiveTreeView.ForgotPassiveButtonEnable(false);

        ChangeSkillPointsText(_passiveTree.SkillPoints);
        _passiveTree.DeselectPassive();
    }

    public void Disable()
    {
        foreach (IPassiveView passiveView in _passiveViews)
        {
            passiveView.OnClick -= () => SelectPassive(passiveView.PassivePresenter.Passive);
        }

        _passiveTreeView.OnClickLearn -= LearnSelectedPassive;
        _passiveTreeView.OnClickForgot -= ForgotSelectedPassive;
        _passiveTreeView.OnClickForgotAll -= ForgotAllPassives;
        _passiveTreeView.OnClickAddSkillPoint -= AddSkillPoint;
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

    private void AddSkillPoint()
    {
        _passiveTree.AddSkillPoint(1);
        UpdatePassiveTreeViewButtons(_passiveTree.SelectedPassive);
    }

    private void SelectPassive(Passive passive)
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