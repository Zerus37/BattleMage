using UnityEngine;

public class SpellsUI : MonoBehaviour
{
	[SerializeField] private Transform _spellBarContainer;
	[SerializeField] private SpellIcon _spellCellPrefab;

	private SpellSO[] _allSpells;

	public void Start()
	{
		SpellSO[] _allSpells = Resources.LoadAll<SpellSO>("Spells");

		foreach(SpellSO spell in _allSpells)
		{
			Instantiate(_spellCellPrefab, _spellBarContainer).SetSO(spell);
		}
	}
}
