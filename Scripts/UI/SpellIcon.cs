using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SpellIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _manaCostText;
	[SerializeField] private TextMeshProUGUI _keyKodeText;
	private SpellSO _so;
	public void SetSO(SpellSO so)
	{
		_so = so;
		_icon.sprite = so.sprite;
		_manaCostText.text = so.manaCost.ToString();
		_keyKodeText.text = "";
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SpellInfoPopup.Show(_so, eventData.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SpellInfoPopup.Hide();
	}
}