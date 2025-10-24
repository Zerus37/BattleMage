using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCast : MonoBehaviour
{
	[SerializeField] protected float _manaUse;
	public float ManaNeed => _manaUse;

	public virtual void Activate(Player player)
	{
		 
	}
}
