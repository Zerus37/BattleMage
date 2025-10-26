using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private Transform _teleportPoint;
	[SerializeField] private float _blindTime = 16;
	[SerializeField] private bool _inverseSpeed = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PostProcessManager.Teleport(_blindTime);
			other.transform.position = _teleportPoint.position;

			if (_inverseSpeed)
				other.GetComponent<Rigidbody>().velocity *= -1;
		}
		else
			Destroy(other.gameObject);
	}
}