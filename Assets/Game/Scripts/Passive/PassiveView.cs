using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveView : MonoBehaviour, IPassiveView
{
    private readonly Color _learnedColor = Color.green;
    private readonly Color _notLearnedColor = Color.white;

    public event Action OnClick;

    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _bordersImage;
    [SerializeField] private TMP_Text _displayNameText;

    private void Awake()
    {
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
