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
	[SerializeField] private DamageTypeScalePair[] _typeScale;

	[SerializeField, HideInInspector] private Dictionary<DamageType, float> _damageScaleDict = new Dictionary<DamageType, float>();
	[SerializeField, HideInInspector] private Rigidbody _rb;

	public void TakeDamage(float damage, DamageType type)
	{
		if (_damageScaleDict.ContainsKey(type))
			damage *= _damageScaleDict[type];

		damage *= _damageScale;

		_hp.TakeDamage(damage);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log($"{gameObject.name} ===> {collision.gameObject.name}");

		if(collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
		{
			Vector3 speed = rb.velocity;
			if (_rb != null)
				speed -= _rb.velocity;

			if (speed.sqrMagnitude < 100)
				return;

			TakeDamage(speed.sqrMagnitude / 100 * rb.mass, DamageType.physycal);
		}
		else
		{
			if (_rb.velocity.sqrMagnitude < 650)
				return;

			TakeDamage(_rb.velocity.sqrMagnitude / 100, DamageType.physycal);
		}
	}



	private void OnValidate()
	{
		_rb = GetComponent<Rigidbody>();

		_damageScaleDict.Clear();
		foreach (var pair in _typeScale)
			_damageScaleDict.Add(pair.type, pair.scale);
	}
}
