using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 1)]
public class SpellSO : ScriptableObject
{
	public MagicScool scool;
	public SpellType type;
	public string spellName;
	public Sprite sprite;
	public int manaCost;

	public Projectile projectile;
	public SelfCast selfCastComponent;

	[TextArea(10, 10)] public string description;
}