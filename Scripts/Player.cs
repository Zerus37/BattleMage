using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private MagicGun _projectileTrow;
	[SerializeField] private GravyGun _gravyGun;
	[SerializeField] private Spell[] _spells;
	[SerializeField] private Mana _mana;

	private Spell currentSpell;


	private int _currentSpellIndex = 0;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			_currentSpellIndex = (_currentSpellIndex + 1) % _spells.Length;

			currentSpell = _spells[_currentSpellIndex];
			switch (currentSpell.type)
			{
				case SpellType.projectile:
					_projectileTrow.SetProjectile(currentSpell.projectile);

					_projectileTrow.enabled = true;
					_gravyGun.enabled = false;
					break;
				case SpellType.gravygun:
					_projectileTrow.enabled = false;
					_gravyGun.enabled = true;
					break;
				case SpellType.selfCast:
					_projectileTrow.enabled = false;
					_gravyGun.enabled = false;
					break;
			}
		}

		if (Input.GetMouseButtonDown(0) && currentSpell.type == SpellType.selfCast)
		{
			if (!_mana.TakeMana(currentSpell.selfCastComponent.ManaNeed))
				return;

			currentSpell.selfCastComponent.Activate();
		}
	}

	private void Start()
	{
		currentSpell = _spells[0];
	}
}
