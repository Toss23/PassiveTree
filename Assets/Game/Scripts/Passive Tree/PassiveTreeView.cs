using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveTreeView : MonoBehaviour, IPassiveTreeView
{
    public event Action OnClickLearn;
    public event Action OnClickForgot;
    public event Action OnClickForgotAll;
    public event Action OnClickAddSkillPoint;

    [SerializeField] private Button _learnPassiveButton;
    [SerializeField] private Button _forgotPassiveButton;
    [SerializeField] private Button _forgotAllPassivesButton;
    [SerializeField] private Button _addSkillPointButton;
    [SerializeField] private TMP_Text _skillPointsText;
    [SerializeField] private TMP_Text _skillPointsCostText;

    private void Awake()
    {
        _learnPassiveButton.onClick.AddListener(() => OnClickLearn());
        _forgotPassiveButton.onClick.AddListener(() => OnClickForgot());
        _forgotAllPassivesButton.onClick.AddListener(() => OnClickForgotAll());
        _addSkillPointButton.onClick.AddListener(() => OnClickAddSkillPoint());
    }

    public void SetSkillPointsText(string text)
    {
        _skillPointsText.text = text;
    }

    public void SetSkillPointsCostText(string text)
    {
        _skillPointsCostText.text = text;
    }

    public void LearnPassiveButtonEnable(bool enable)
    {
        _learnPassiveButton.interactable = enable;
    }

    public void ForgotPassiveButtonEnable(bool enable)
    {
        _forgotPassiveButton.interactable = enable;
    }
}