using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aura : SelfCast
{
	protected bool _on = false;
	protected UnityEvent _onActivate = new UnityEvent();
	protected UnityEvent _onDeActivate = new UnityEvent();

	public override void Activate(Player player, int altVariant = 0)
	{
		base.Activate(player, altVariant);

		_on = !_on;

		if (_on)
			_onActivate.Invoke();
		else
			_onDeActivate.Invoke();
	}

	protected virtual void FixedUpdate()
	{
		if (!_on) return;
		if (_manaUse == 0) return;

		if (!_player.Mana.TakeMana(_manaUse * Time.fixedDeltaTime))
		{
			_on = false;
			return;
		}
	}

	private void OnDestroy()
	{
		_onActivate.RemoveAllListeners();
		_onDeActivate.RemoveAllListeners();
	}
}
