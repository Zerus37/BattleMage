using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsUI : MonoBehaviour
{
	private static SpellsUI instance;

	[SerializeField] private Player _player;
	[SerializeField] private Transform _spellBarContainer;
	[SerializeField] private SpellIcon _spellCellPrefab;

	private List<SpellIcon> _allSpells = new List<SpellIcon>();
	private SpellIcon _selectedSpell;
	private Dictionary<KeyCode, SpellIcon> _keyIconDict = new Dictionary<KeyCode, SpellIcon>();
	private HashSet<KeyCode> _tabooKeys = new HashSet<KeyCode>
	{
		KeyCode.W,
		KeyCode.A,
		KeyCode.S,
		KeyCode.D,

		KeyCode.LeftShift
	};

	public void Start()
	{
		instance = this;
		SpellSO[] _allSpellsSO = Resources.LoadAll<SpellSO>("Spells");

		foreach(SpellSO spell in _allSpellsSO)
		{
			_allSpells.Add(Instantiate(_spellCellPrefab, _spellBarContainer));
			_allSpells[_allSpells.Count - 1].SetSO(spell);
		}
	}

	public static void SelectSpell(SpellIcon selected)
	{
		instance._selectedSpell = selected;
	}

	void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown
			&& e.keyCode != KeyCode.None
			&& !_tabooKeys.Contains(e.keyCode))
		{
			Debug.Log("Detected key code: " + e.keyCode);

			if (_keyIconDict.ContainsKey(e.keyCode))
			{
				_keyIconDict[e.keyCode].SetKeyString("");
				_keyIconDict.Remove(e.keyCode);
			}

			foreach(KeyValuePair<KeyCode, SpellIcon> pair in _keyIconDict)
			{
				if(pair.Value == _selectedSpell)
				{
					_keyIconDict.Remove(pair.Key);
					break;
				}
			}
			
			_selectedSpell.SetKeyString(((char)e.keyCode).ToString());
			_keyIconDict.Add(e.keyCode, _selectedSpell);
		}
	}
}
