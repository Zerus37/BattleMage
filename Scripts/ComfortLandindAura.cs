using UnityEngine;

public class ComfortLandindAura : SelfCast
{
	[SerializeField] private Rigidbody _playerRb;

	private bool _on = false;
	private Player _player;

	public override void Activate(Player player)
	{
		_on = !_on;
		_player = player;
	}

	private void FixedUpdate()
	{
		if (!_on) return;
		if (!_player.Mana.TakeMana(_manaUse * Time.fixedDeltaTime)) return;

		Vector3 velocity = _playerRb.velocity;
		velocity.y = Mathf.Clamp(velocity.y , -15, 100f);

		_playerRb.velocity = velocity;
	}
}
