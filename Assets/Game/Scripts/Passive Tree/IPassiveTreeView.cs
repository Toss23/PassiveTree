using System;

public interface IPassiveTreeView
{
    public event Action OnClickLearn;
    public event Action OnClickForgot;
    public event Action OnClickForgotAll;
    public event Action OnClickAddSkillPoint;

    public void SetSkillPointsText(string text);
    public void SetSkillPointsCostText(string text);
    public void LearnPassiveButtonEnable(bool enable);
    public void ForgotPassiveButtonEnable(bool enable);
}