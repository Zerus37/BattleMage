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
			StartCoroutine(Go());
	}

	private IEnumerator Go()
	{
		_movement.enabled = false;
		_rb.useGravity = false;
		_hitCollider.ScaleDamageSpeedLimit(8);
		Vector3 moveVector = Camera.main.transform.forward * _distance;

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
