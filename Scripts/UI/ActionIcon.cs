using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ActionIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public PlayerAction mainAction;

	[SerializeField] protected TextMeshProUGUI _keyKodeText;

	public void SetKeyString(string value)
	{
		_keyKodeText.text = value;
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(this);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		KeyBindingSystem.SelectAction(null);
	}
}
