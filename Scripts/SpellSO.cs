using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 1)]
public class SpellSO : ScriptableObject
{
	public MagicScool scool;
	public string spellName;
	public Sprite sprite;
	public int manaCost;

	[TextArea(10, 10)] public string description;
}