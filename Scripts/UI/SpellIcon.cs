using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellIcon : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _manaCostText;
	[SerializeField] private TextMeshProUGUI _keyKodeText;
	private SpellSO _so;

	private void SetSO(SpellSO so)
	{
		_so = so;
		_icon.sprite = so.sprite;
		_manaCostText.text = so.manaCost.ToString();
	}
}