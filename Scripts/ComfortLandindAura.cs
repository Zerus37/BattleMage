using UnityEngine;

public class ComfortLandindAura : Aura
{
	[SerializeField] private GameObject _effect;

	private void Start()
	{
		_onActivate.AddListener(() => _effect.SetActive(true));
		_onDeActivate.AddListener(() => _effect.SetActive(false));
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		Vector3 velocity = _player.Rigidbody.velocity;
		velocity.y = Mathf.Clamp(velocity.y , -15, 100f);

		_player.Rigidbody.velocity = velocity;
	}
}
