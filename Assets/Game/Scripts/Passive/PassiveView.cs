using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveView : MonoBehaviour, IPassiveView
{
    private readonly Color _learnedColor = Color.green;
    private readonly Color _notLearnedColor = Color.white;

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

        _button.onClick.AddListener(OnClickView);
    }

    public void SetDisplayName(string displayName)
    {
        _displayNameText.text = displayName;
    }

    public void SetIconColor(IconColor iconColor)
    {
        switch (iconColor)
        {
            case IconColor.OnLearnedColor:
                _iconImage.color = _learnedColor;
                break;
            case IconColor.OnNotLearnedColor:
                _iconImage.color = _notLearnedColor;
                break;
        }
    }

    public void BordersActive(bool active)
    {
        _bordersImage.gameObject.SetActive(active);
    }

    private void OnClickView()
    {
        OnClick?.Invoke();
    }
}
