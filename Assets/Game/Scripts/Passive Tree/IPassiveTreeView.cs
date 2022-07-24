﻿using System;

public interface IPassiveTreeView
{
    public event Action OnClickLearn;
    public event Action OnClickForgot;
    public event Action OnClickForgotAll;
    public event Action OnClickAddSkillPoint;

    public void SetSkillPointText(string text);
}