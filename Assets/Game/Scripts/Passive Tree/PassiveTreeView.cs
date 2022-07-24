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

    private void Awake()
    {
        _learnPassiveButton.onClick.AddListener(() => OnClickLearn());
        _forgotPassiveButton.onClick.AddListener(() => OnClickForgot());
        _forgotAllPassivesButton.onClick.AddListener(() => OnClickForgotAll());
        _addSkillPointButton.onClick.AddListener(() => OnClickAddSkillPoint());
    }

    public void SetSkillPointText(string text)
    {
        _skillPointsText.text = text;
    }
}