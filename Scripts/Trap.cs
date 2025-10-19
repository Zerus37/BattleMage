using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private float _damage;
	[SerializeField] private DamageType _type;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<HitCollider>(out HitCollider target))
			target.TakeDamage(_damage, _type);
	}
}