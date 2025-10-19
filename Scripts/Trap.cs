using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private float _damage;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<HP>(out HP target))
			target.TakeDamage(_damage);
	}
}