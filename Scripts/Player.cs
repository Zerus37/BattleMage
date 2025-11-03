using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[SerializeField] private MagicGun _magicGun;
	[SerializeField] private GravyGun _gravyGun;
	[SerializeField] private Mana _mana;
	[SerializeField] private GameObject _escMenu;
	[SerializeField] private Transform _magicComponentContainer;

	[SerializeField] private List<MonoBehaviour> _components = new List<MonoBehaviour>();


	[SerializeField] private FirstPersonMovement _movement;
	[SerializeField] private HitCollider _hitCollider;
	[SerializeField] private Rigidbody _rb;


	private KeyBindingSystem _keysSystem;
	private bool _pause = false;
	private Dictionary<PlayerAction, SelfCast> _actionComponentsDict = new Dictionary<PlayerAction, SelfCast>();
	private float _timeScaleBeforePause = 1;

	public Mana Mana => _mana;
	public FirstPersonMovement Movement => _movement;
	public HitCollider HitCollider => _hitCollider;
	public Rigidbody Rigidbody => _rb;
	public bool Pause => _pause;

	public void SetKeysSystem(KeyBindingSystem link)
	{
		_keysSystem = link;

		SelfCast cast = null;
		foreach (PlayerAction playerAction in _keysSystem.AllPlayerAction)
		{
			if (playerAction.type == ActionType.spell &&
				playerAction.so.type == SpellType.selfCast)
			{
				if (playerAction.altVariant < 0)
				{
					cast = Instantiate(playerAction.so.selfCastComponent, _magicComponentContainer);
					cast.SetPlayer(this);
					cast.SetManaUse(playerAction.so.manaCost);

					_components.Add(cast);
				}

				_actionComponentsDict.Add(playerAction, cast);
			}
		}
	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.E))
		//{
		//	_currentSpellIndex = (_currentSpellIndex + 1) % _spells.Count;

		//	currentSpell = _spells[_currentSpellIndex];
		//	switch (currentSpell.type)
		//	{
		//		case SpellType.projectile:
		//			_projectileTrow.SetProjectile(currentSpell.projectile);

		//			_projectileTrow.enabled = true;
		//			_gravyGun.enabled = false;
		//			break;
		//		case SpellType.gravygun:
		//			_projectileTrow.enabled = false;
		//			_gravyGun.enabled = true;
		//			break;
		//		case SpellType.selfCast:
		//			_projectileTrow.enabled = false;
		//			_gravyGun.enabled = false;
		//			break;
		//	}
		//}

		//if (Input.GetMouseButtonDown(0) && currentSpell.type == SpellType.selfCast)
		//{
		//	currentSpell.selfCastComponent.Activate(this);
		//}

		if (Input.GetKeyDown(KeyCode.Escape))
			SetPause(!_pause);
	}

	void OnGUI()
	{
		if (_pause)
			return;

		Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown
			&& e.keyCode != KeyCode.None
			&& !_keysSystem.IgnoreThisKey(e.keyCode))
		{
			if (!_keysSystem.KeyPlayerActionDict.ContainsKey(e.keyCode))
				return;

			PlayerAction playerAction = _keysSystem.KeyPlayerActionDict[e.keyCode];

			if(playerAction.type == ActionType.spell)
			{
				switch(playerAction.so.type)
				{
					case SpellType.projectile:
						_magicGun.Shoot(playerAction.so.projectile);
						break;
					case SpellType.selfCast:
						_actionComponentsDict[playerAction].Activate(this, playerAction.altVariant);
						break;
				}
			}
		}
	}

	public void SetPause(bool flag)
	{
		_pause = flag;
		_escMenu.SetActive(flag);
		foreach (MonoBehaviour comp in _components)
			comp.enabled = !flag;

		if (flag)
		{
			_timeScaleBeforePause = Time.timeScale;
			Time.timeScale = 0;
		}
		else
			Time.timeScale = _timeScaleBeforePause;

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
		_escMenu.SetActive(false);
	}
}
