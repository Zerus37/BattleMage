using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerAction
{
	public ActionType type;
	public SpellSO so;
	public ActionIcon ui;
	public int altVariant;

	public PlayerAction(ActionType type, SpellSO so, ActionIcon ui)
	{
		this.type = type;
		this.so = so;
		this.ui = ui;
		altVariant = -1;
		ui.mainAction = this;
	}

	public PlayerAction(ActionType type, SpellSO so, ActionIcon ui, int altVariant)
	{
		this.type = type;
		this.so = so;
		this.ui = ui;
		this.altVariant = altVariant;
		ui.mainAction = this;
	}
}

public class KeyBindingSystem : MonoBehaviour
{
	private static KeyBindingSystem instance;

	[SerializeField] private Player _player;
	[SerializeField] private Transform _spellBarContainer;
	[SerializeField] private SpellIcon _spellCellPrefab;

	private List<PlayerAction> _allPlayerAction = new List<PlayerAction>();
	private ActionIcon _selectedAction;

	private Dictionary<KeyCode, PlayerAction> _keyPlayerActionDict = new Dictionary<KeyCode, PlayerAction>();
	private HashSet<KeyCode> _tabooKeys = new HashSet<KeyCode>
	{
		KeyCode.W,
		KeyCode.A,
		KeyCode.S,
		KeyCode.D,

		KeyCode.LeftShift,
		KeyCode.Escape
	};

	public bool IgnoreThisKey(KeyCode key) { return _tabooKeys.Contains(key); }

	public Dictionary<KeyCode, PlayerAction> KeyPlayerActionDict => _keyPlayerActionDict;
	public List<PlayerAction> AllPlayerAction => _allPlayerAction;

	public void Start()
	{
		instance = this;
		SpellSO[] _allSpellsSO = Resources.LoadAll<SpellSO>("Spells");

		foreach(SpellSO spell in _allSpellsSO)
		{
			SpellIcon icon = Instantiate(_spellCellPrefab, _spellBarContainer);
			icon.SetSO(spell);

			_allPlayerAction.Add(new PlayerAction(ActionType.spell, spell, icon));

			if (spell.type == SpellType.selfCast
				&& spell.selfCastComponent.AltVariantCounts > 0)
			{
				for(int i = 0; i < spell.selfCastComponent.AltVariantCounts; i++)
				{
					icon = Instantiate(_spellCellPrefab, _spellBarContainer);
					icon.SetSO(spell, i);

					_allPlayerAction.Add(new PlayerAction(ActionType.spell, spell, icon, i));
				}
			}
		}

		_player.SetKeysSystem(this);
	}

	public static void SelectAction(ActionIcon selected)
	{
		instance._selectedAction = selected;
	}

	void OnGUI()
	{
		if (_selectedAction == null)
			return;

		Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown
			&& e.keyCode != KeyCode.None
			&& !_tabooKeys.Contains(e.keyCode))
		{
			Debug.Log("Detected key code: " + e.keyCode);

			if (_keyPlayerActionDict.ContainsKey(e.keyCode))
			{
				if(_keyPlayerActionDict[e.keyCode].type == ActionType.spell)
					_keyPlayerActionDict[e.keyCode].ui.SetKeyString("");

				_keyPlayerActionDict.Remove(e.keyCode);
			}

			foreach(KeyValuePair<KeyCode, PlayerAction> pair in _keyPlayerActionDict)
			{
				if(pair.Value.ui == _selectedAction)
				{
					_keyPlayerActionDict.Remove(pair.Key);
					break;
				}
			}

			_selectedAction.SetKeyString(((char)e.keyCode).ToString());



			_keyPlayerActionDict.Add(e.keyCode, _selectedAction.mainAction);
		}
	}
}
