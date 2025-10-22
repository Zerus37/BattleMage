using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	[SerializeField] private Rigidbody[] _rbs;
	[SerializeField] private Rigidbody _rootPart;

	public Rigidbody Root => _rootPart;

	public void Run()
	{
		foreach(Rigidbody rb in _rbs)
		{
			rb.isKinematic = false;
			rb.constraints = RigidbodyConstraints.None;
		}
	}

	public void Freze(bool flag)
	{
		foreach (Rigidbody rb in _rbs)
		{
			rb.isKinematic = flag;
			rb.useGravity = !flag;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		_rootPart.isKinematic = false;
	}

	public void Push(Vector3 moveVector)
	{
		foreach (Rigidbody rb in _rbs)
		{
			rb.AddForce(moveVector, ForceMode.VelocityChange);
		}
	}
}
