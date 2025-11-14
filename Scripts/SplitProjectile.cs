using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitProjectile : Projectile
{
	[SerializeField] private float offset = 0.1f;

	[SerializeField] protected ParticleSystem[] _particles;
	[SerializeField] protected Projectile[] _childProjectiles;


	protected override void OnCollisionEnter(Collision collision)
	{
		OffParticles();
		_hitParticle.Play();

		if (collision.gameObject.TryGetComponent<HitCollider>(out HitCollider target))
		{
			target.TakeDamage(_damage, _type);
		}

		Vector3 normal = collision.contacts[0].normal;

		if(_childProjectiles.Length == 1)
		{
			Instantiate(_childProjectiles[0],
				collision.contacts[0].point - normal * offset,
				transform.rotation
				).RB.AddForce(Vector3.Reflect(RB.velocity, normal));
		}
		else if(_childProjectiles.Length > 1)
		{
			float angle = 360 / _childProjectiles.Length;

			for (int i = 0; i < _childProjectiles.Length; i++)
			{
				Vector3 spawnPoint = collision.contacts[0].point + normal * offset + Quaternion.AngleAxis(angle * i, normal) * transform.right * offset;
				Vector3 spawnOffset = spawnPoint - collision.contacts[0].point;
				Quaternion rotation = Quaternion.LookRotation(spawnOffset, Vector3.up);

				Instantiate(_childProjectiles[i],
					spawnPoint,
					rotation
					).RB.AddForce(spawnOffset.normalized * _childProjectiles[i].StartSpeed, ForceMode.VelocityChange);
			}
		}

		if (!_collisionLimitOn)
			return;

		_collisionLimit -= 1;
		if (_collisionLimit <= 0)
		{
			CancelInvoke();
			TurnOff();
		}
	}

	protected override void Start()
	{
		base.Start();

		transform.Rotate(0, 0, Random.Range(0f, 360f), Space.Self);
	}

	public override void TurnOff()
	{
		base.TurnOff();
		OffParticles();
	}

	private void OffParticles()
	{
		foreach(ParticleSystem effect in _particles)
		{
			effect.Stop();
		}
	}
}
