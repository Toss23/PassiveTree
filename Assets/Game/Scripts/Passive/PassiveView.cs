using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveView : MonoBehaviour, IPassiveView
{
    private Color _learnedColor = Color.green;
    private Color _notLearnedColor = Color.white;

    public event Action OnClick;

    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _bordersImage;
    [SerializeField] private TMP_Text _displayNameText;

    private void Awake()
    {
        _button.onClick.AddListener(OnClickPassive);
    }

    public void SetDisplayName(string displayName)
    {
        _displayNameText.text = displayName;
    }

    public void SetIconState(bool learned)
    {
        _iconImage.color = learned ? _learnedColor : _notLearnedColor;
    }

    public void BordersActive(bool active)
    {
        _bordersImage.gameObject.SetActive(active);
    }

    private void OnClickPassive()
    {
        OnClick?.Invoke();
    }
}
