using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SpellIcon : ActionIcon, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _manaCostText;
	private SpellSO _so;
	private int _altVariant = -1;
	

	public void SetSO(SpellSO so)
	{
		_so = so;
		_icon.sprite = so.sprite;
		_manaCostText.text = so.manaCost.ToString();
		_keyKodeText.text = "";
	}

	public void SetSO(SpellSO so, int variant)
	{
		SetSO(so);
		_altVariant = variant;
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(this);

		if(_altVariant < 0)
			SpellInfoPopup.Show(_so, eventData.position);
		else
			SpellInfoPopup.Show(_so, eventData.position, _altVariant);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(null);
		SpellInfoPopup.Hide();
	}
}