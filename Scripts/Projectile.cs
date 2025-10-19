using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float _lifetime;
	[SerializeField] private float _manaUse;
	[SerializeField] private float _damage;
	[SerializeField] private Rigidbody _rb;
	public float ManaUse => _manaUse;
	public Rigidbody RB => _rb;

	private void Start()
	{
		Destroy(gameObject, _lifetime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent<HP>(out HP target))
		{
			target.TakeDamage(_damage);
		}
	}
}