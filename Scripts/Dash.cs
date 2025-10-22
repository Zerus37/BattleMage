using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	[SerializeField] private float _distance;
	[SerializeField] private float _time;

	[SerializeField] private FirstPersonMovement _movement;
	[SerializeField] private Mana _mana;
	[SerializeField] private HitCollider _hitCollider;
	[SerializeField] private Rigidbody _rb;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && _mana.TakeMana(10))
			StartCoroutine(Go(Directions.forward));

		if (Input.GetKeyDown(KeyCode.Z) && _mana.TakeMana(10))
			StartCoroutine(Go(Directions.left));
		if (Input.GetKeyDown(KeyCode.X) && _mana.TakeMana(10))
			StartCoroutine(Go(Directions.back));
		if (Input.GetKeyDown(KeyCode.C) && _mana.TakeMana(10))
			StartCoroutine(Go(Directions.right));
	}

	private IEnumerator Go(Directions direction)
	{
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
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
