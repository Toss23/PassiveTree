using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveView : MonoBehaviour, IPassiveView
{
    private readonly Dictionary<IconColor, Color> _iconColor = new Dictionary<IconColor, Color>()
    {
        { IconColor.OnLearnedColor, Color.green },
        { IconColor.OnNotLearnedColor, Color.white }
    };

    public event Action OnClick;

    [SerializeField] private PassiveData _passiveData;
    [Space(10)]
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _bordersImage;
    [SerializeField] private TMP_Text _displayNameText;

    private PassivePresenter _passivePresenter;

    public PassivePresenter PassivePresenter { get { return _passivePresenter; } }

    private void Awake()
    {
        Passive passive = new Passive(_passiveData);
        _passivePresenter = new PassivePresenter(this, passive);
        _passivePresenter.Initialize();
        _passivePresenter.Enable();

        _button.onClick.AddListener(() => OnClick());
    }

    public void SetDisplayName(string displayName)
    {
        _displayNameText.text = displayName;
    }

    public void SetIconColor(IconColor iconColor)
    {
        _iconImage.color = _iconColor[iconColor];
    }

    public void BordersActive(bool active)
    {
        _bordersImage.gameObject.SetActive(active);
    }
}
