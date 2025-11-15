using System.Collections;
using UnityEngine;

public class BulletTime : SelfCast
{
	[SerializeField] private float _slowTime = 10f;
	private bool _on = false;

	private void OnDestroy()
	{
		Time.timeScale = 1f;
		StopAllCoroutines();
	}

	public override void Activate(Player player, int altVariant = 0)
	{
		if (_on) return;

		base.Activate(player, altVariant);
		if (!player.Mana.TakeMana(_manaUse)) return;

		StartCoroutine(ApplyEffect());
	}

	private IEnumerator ApplyEffect()
	{
		Time.fixedDeltaTime = 0.005f;

		_on = true;
		PostProcessManager.SpeedUp();
		Time.timeScale = 0.25f;

		float t = 0;
		while(t < _slowTime)
		{
			if (!_player.Pause)
				t += Time.unscaledDeltaTime;

			yield return null;
		}

		Time.timeScale = 1f;
		PostProcessManager.Base();
		_on = false;

		Time.fixedDeltaTime = 0.02f;
	} 
}
