using System.Collections;
using UnityEngine;

public class BulletTime : SelfCast
{
	private void OnDisable()
	{
		Time.timeScale = 1f;
		StopAllCoroutines();
	}

	public override void Activate()
	{
		StartCoroutine(ApplyEffect());
	}

	private IEnumerator ApplyEffect()
	{
		PostProcessManager.SpeedUp();
		Time.timeScale = 0.25f;
		yield return new WaitForSecondsRealtime(10f);
		Time.timeScale = 1f;
		PostProcessManager.Base();
	} 
}
