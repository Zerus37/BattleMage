using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : SelfCast
{
	[SerializeField] private float _distance;
	[SerializeField] private float _time;

	private FirstPersonMovement _movement;
	private HitCollider _hitCollider;
	private Rigidbody _rb;
	private bool _busy = false;

	public override void Activate(Player player, int altVariant = 0)
	{
		if (_busy)
			return;

		base.Activate(player, altVariant);

		if (!player.Mana.TakeMana(_manaUse)) return;

		switch (altVariant)
		{
			case 0:
				StartCoroutine(Go(Directions.left));
				break;
			case 1:
				StartCoroutine(Go(Directions.back));
				break;
			case 2:
				StartCoroutine(Go(Directions.right));
				break;

			default:
				StartCoroutine(Go(Directions.forward));
				break;
		}
	}

	public override void SetPlayer(Player player)
	{
		_movement = player.Movement;
		_hitCollider = player.HitCollider;
		_rb = player.Rigidbody;
	}

	private IEnumerator Go(Directions direction)
	{
		_busy = true;
		_movement.enabled = false;
		_rb.useGravity = false;
		_hitCollider.ScaleDamageSpeedLimit(8);
		Vector3 moveVector = Vector3.zero;

		switch (direction)
		{
			case Directions.forward:
				moveVector = Camera.main.transform.forward * _distance;
				break;

			case Directions.left:
				moveVector = -Camera.main.transform.right * _distance;
				break;
			case Directions.back:
				moveVector = -Camera.main.transform.forward * _distance;
				break;
			case Directions.right:
				moveVector = Camera.main.transform.right * _distance;
				break;
		}


		for (float t = 0; t < 1; t += Time.deltaTime / _time)
		{
			_rb.velocity = moveVector / _time;
			yield return null;
		}

		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
		_hitCollider.ScaleDamageSpeedLimit(0.125f);
		_rb.useGravity = true;
		_movement.enabled = true;
		_busy = false;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
