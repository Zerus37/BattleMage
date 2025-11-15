using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCast : MonoBehaviour
{
	[SerializeField] protected float _manaUse;
	[SerializeField] protected string[] _altVariantsAffixes;
	public float ManaNeed => _manaUse;
	public int AltVariantCounts => _altVariantsAffixes.Length;
	protected Player _player;

	public string GetAffix(int index)
	{
		if(index < 0 || index >= _altVariantsAffixes.Length)
		{
			Debug.LogWarning("SelfCast GetAffix wrong index, return empty string");
			return "";
		}

		return _altVariantsAffixes[index];
	}

	public virtual void Activate(Player player, int altVariant = 0)
	{
		if (altVariant > _altVariantsAffixes.Length) { Debug.LogWarning("SelfCast altVariant must by in range in varible. Use deafoult"); altVariant = 0;}
	}

	public virtual void SetPlayer(Player player)
	{
		this._player = player;
	}

	public virtual void SetManaUse(float value)
	{
		_manaUse = value;
	}
}
