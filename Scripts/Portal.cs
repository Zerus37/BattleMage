using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private Transform _teleportPoint;
	[SerializeField] private float _blindTime = 16;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PostProcessManager.Teleport(_blindTime);
			other.transform.position = _teleportPoint.position;
		}
		else
			Destroy(other.gameObject);
	}
}