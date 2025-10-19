using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	[SerializeField] private Rigidbody[] _rbs;

	public void Run()
	{
		foreach(Rigidbody rb in _rbs)
		{
			rb.isKinematic = false;
			rb.constraints = RigidbodyConstraints.None;
		}
	}
}
