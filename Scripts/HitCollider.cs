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

	public void TakeDamage(float damage, DamageType type)
	{
		if (_damageScaleDict.ContainsKey(type))
			damage *= _damageScaleDict[type];

		damage *= _damageScale;

		_hp.TakeDamage(damage);
	}



	private void OnValidate()
	{
		_damageScaleDict.Clear();
		foreach (var pair in _typeScale)
			_damageScaleDict.Add(pair.type, pair.scale);
	}
}
