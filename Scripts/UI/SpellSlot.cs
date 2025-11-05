using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
	[SerializeField] private Image _spellImage;
	private PlayerAction _action;

	public PlayerAction Action => _action;

	public void SetPlayerAction(PlayerAction Action)
	{
		if(Action.type != ActionType.spell)
		{
			Debug.LogError("SpellSlot SetPlayerAction must by ActionType.spell");
			return;
		}

		_action = Action;
		_spellImage.color = Color.white;
		_spellImage.sprite = Action.so.sprite;
	}
}
