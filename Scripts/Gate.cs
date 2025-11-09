using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
	[SerializeField] private Animator _anim;
	[SerializeField] private Collider _collider;
	[SerializeField] private UnityEvent _onOpen;
	[SerializeField] private bool _playerOnly = true;

	void OnTriggerEnter(Collider other)
	{
		if (_playerOnly && !other.CompareTag("Player"))
			return;

		Destroy(_collider);
		_anim.SetBool("DoorOpen", true);
		_anim.SetBool("DoorClose", false);
		_onOpen.Invoke();
		StartCoroutine(TurnOff());
	}

	public IEnumerator TurnOff()
	{
		yield return new WaitForSeconds(2);

		Destroy(_anim);
		Destroy(this);
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
