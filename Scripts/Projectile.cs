using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private DamageType _type;
	[SerializeField] private float _lifetime;
	[SerializeField] private float _manaUse;
	[SerializeField] private float _damage;
	[SerializeField] private float _startSpeed;
	[SerializeField] private bool _collisionLimitOn = true;
	[SerializeField] private int _collisionLimit = 3;
	[SerializeField] private bool _turnOffEffect;

	[SerializeField, HideInInspector] private Rigidbody _rb;
	[SerializeField, HideInInspector] private SphereCollider _collider;
	[SerializeField, HideInInspector] private ParticleSystem _mainParticle;
	[SerializeField, HideInInspector] private ParticleSystem _hitParticle;
	public float ManaUse => _manaUse;
	public float StartSpeed => _startSpeed;
	public Rigidbody RB => _rb;

	private void OnCollisionEnter(Collision collision)
	{
		_hitParticle.Play();

		if (collision.gameObject.TryGetComponent<HitCollider>(out HitCollider target))
		{
			target.TakeDamage(_damage, _type);
		}

		if (!_collisionLimitOn)
			return;

		_collisionLimit -= 1;
		if(_collisionLimit <= 0)
		{
			CancelInvoke();
			TurnOff();
		}
	}



	public void TurnOff()
	{
		CancelInvoke();
		Destroy(_rb);
		Destroy(_collider);
		_mainParticle.Stop();

		if (_turnOffEffect)
			_hitParticle.Play();

		Destroy(gameObject, 4);
	}

	private void OnValidate()
	{
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<SphereCollider>();

		var p = transform.Find("Main Particle");

		if (_rb == null)
			Debug.LogError("Projectile must have Rigidbody");
		if (_collider == null)
			Debug.LogError("Projectile must have SphereCollider");
		if (p == null)
		{
			Debug.LogError("Projectile must have child named 'Main Particle'");
			return;
		}
		_mainParticle = p.GetComponent<ParticleSystem>();

		p = transform.Find("Hit Particle");
		if (p == null)
		{
			Debug.LogError("Projectile must have child named 'Hit Particle'");
			return;
		}

		_hitParticle = p.GetComponent<ParticleSystem>();
	}

	private void Start()
	{
		Invoke(nameof(TurnOff), _lifetime);
	}

	private void OnDisable()
	{
		CancelInvoke();
	}
}