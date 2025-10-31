using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[SerializeField] private MagicGun _projectileTrow;
	[SerializeField] private GravyGun _gravyGun;
	[SerializeField] private Spell[] _spells;
	[SerializeField] private Mana _mana;
	[SerializeField] private GameObject _escMenu;

	[SerializeField] private List<MonoBehaviour> _components = new List<MonoBehaviour>();

	private Spell currentSpell;
	private int _currentSpellIndex = 0;
	private bool _pause = false;

	public Mana Mana => _mana;
	public bool Pause => _pause;

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
			currentSpell.selfCastComponent.Activate(this);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			SetPause(!_pause);
	}

	public void SetPause(bool flag)
	{
		_pause = flag;
		_escMenu.SetActive(flag);
		foreach (MonoBehaviour comp in _components)
			comp.enabled = !flag;

		Time.timeScale = flag ? 0 : 1;

		if (flag)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void Start()
	{
		currentSpell = _spells[0];
		_escMenu.SetActive(false);
	}
}
