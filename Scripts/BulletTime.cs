using System.Collections;
using UnityEngine;

public class BulletTime : SelfCast
{
	private bool _on = false;

	private void OnDisable()
	{
		Time.timeScale = 1f;
		StopAllCoroutines();
	}

	public override void Activate(Player player)
	{
		if (_on) return;
		if (!player.Mana.TakeMana(_manaUse)) return;

		StartCoroutine(ApplyEffect());
	}

	private IEnumerator ApplyEffect()
	{
		Time.fixedDeltaTime = 0.005f;

		_on = true;
		PostProcessManager.SpeedUp();
		Time.timeScale = 0.25f;
		yield return new WaitForSecondsRealtime(10f);
		Time.timeScale = 1f;
		PostProcessManager.Base();
		_on = false;

		Time.fixedDeltaTime = 0.02f;
	} 
}
