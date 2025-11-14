using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] protected DamageType _type;
	[SerializeField] protected float _lifetime;
	[SerializeField] protected float _manaUse;
	[SerializeField] protected float _damage;
	[SerializeField] protected float _startSpeed;
	[SerializeField] protected bool _collisionLimitOn = true;
	[SerializeField] protected int _collisionLimit = 3;
	[SerializeField] protected bool _turnOffEffect;

	[SerializeField, HideInInspector] protected Rigidbody _rb;
	[SerializeField, HideInInspector] protected SphereCollider _collider;
	[SerializeField, HideInInspector] protected ParticleSystem _mainParticle;
	[SerializeField, HideInInspector] protected ParticleSystem _hitParticle;
	public float ManaUse => _manaUse;
	public float StartSpeed => _startSpeed;
	public Rigidbody RB => _rb;

	protected virtual void OnCollisionEnter(Collision collision)
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



	public virtual void TurnOff()
	{
		CancelInvoke();
		Destroy(_rb);
		Destroy(_collider);
		_mainParticle.Stop();

		if (_turnOffEffect)
			_hitParticle.Play();

		Destroy(gameObject, 4);
	}

	protected void OnValidate()
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

	protected virtual void Start()
	{
		Invoke(nameof(TurnOff), _lifetime);
	}

	protected void OnDisable()
	{
		CancelInvoke();
	}
}