using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DamageTypeScalePair
{
	public DamageType type;
	public float scale;
}

public class HitCollider : MonoBehaviour
{
	[SerializeField] private HP _hp;

	[SerializeField] private float _damageScale = 1;
	[SerializeField] private float _minDamageSpeed = 10;
	[SerializeField] private DamageTypeScalePair[] _typeScale;

	[SerializeField, HideInInspector] private Dictionary<DamageType, float> _damageScaleDict = new Dictionary<DamageType, float>();
	[SerializeField, HideInInspector] private float _minDamageSpeedSqrt;

	public void TakeDamage(float damage, DamageType type)
	{
		if (_damageScaleDict.ContainsKey(type))
			damage *= _damageScaleDict[type];

		damage *= _damageScale;

		_hp.TakeDamage(damage);
	}

	public void ScaleDamageSpeedLimit(float scale)
	{
		_minDamageSpeedSqrt *= scale;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.sqrMagnitude < _minDamageSpeedSqrt)
			return;

		if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
		{
			if (rb.mass < 1)
				return;

			TakeDamage(collision.relativeVelocity.sqrMagnitude / 100 * rb.mass, DamageType.physycal);
		}
		else
			TakeDamage(collision.relativeVelocity.sqrMagnitude / 10, DamageType.physycal);
	}



	private void OnValidate()
	{
		_damageScaleDict.Clear();
		foreach (var pair in _typeScale)
			_damageScaleDict.Add(pair.type, pair.scale);

		_minDamageSpeedSqrt = _minDamageSpeed * _minDamageSpeed;
	}
}
