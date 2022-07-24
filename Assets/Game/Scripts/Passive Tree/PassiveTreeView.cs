using System;
using UnityEngine;
using UnityEngine.UI;

public class PassiveTreeView : MonoBehaviour
{
    public event Action OnClickLearn;
    public event Action OnClickForgot;
    public event Action OnClickForgotAll;

    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _forgotButton;
    [SerializeField] private Button _forgotAllButton;

    private void Awake()
    {
        _learnButton.onClick.AddListener(() => OnClickLearn());
        _forgotButton.onClick.AddListener(() => OnClickForgot());
        _forgotAllButton.onClick.AddListener(() => OnClickForgotAll());
    }
}