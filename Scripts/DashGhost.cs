using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhost : MonoBehaviour
{
	[SerializeField] private Monster _monster;
	[SerializeField] private float _dashTime;
	private bool _busy = false;

	public void Dash()
	{
		if (_busy)
			return;

		StartCoroutine(DashRoutine());
	}

	private IEnumerator DashRoutine()
	{
		_monster.Animator.SetBool("Dash", true);
		_busy = true;
		_monster.SetPause(true);

		Vector3 a = transform.position;

		for (float t = 0; t < 1; t+= Time.deltaTime / _dashTime * 2)
		{
			transform.position = Vector3.Lerp(a, _monster.Target.position + Vector3.up, t);
			yield return null;
		}

		_monster.Hit();

		Vector3 b = transform.position;
		Vector3 c = b + (b - a) + (2 * (a.y - b.y) * Vector3.up);

		for (float t = 0; t < 1; t += Time.deltaTime / _dashTime * 2)
		{
			transform.position = Vector3.Lerp(b, c, t);
			yield return null;
		}

		for (float t = 0; t < 1; t += Time.deltaTime / _dashTime * 2)
		{
			transform.position = Vector3.Lerp(c, _monster.Target.position + Vector3.up, t);
			yield return null;
		}

		_monster.Hit();

		b = transform.position;

		for (float t = 0; t < 1; t += Time.deltaTime / _dashTime * 2)
		{
			transform.position = Vector3.Lerp(b, a, t);
			yield return null;
		}

		_monster.Animator.SetBool("Dash", false);
		_monster.SetPause(false);
		_busy = false;
	}

	public void TurnOff()
	{
		_busy = true;
		_monster.Animator.SetTrigger("Die");
		StopAllCoroutines();
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
