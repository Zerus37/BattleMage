using UnityEngine;

public class MagicGun : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown;
    [SerializeField] private Mana _mana;

    private float _lastShootTime = 0;

    public void SetProjectile(Projectile projectile)
	{
        _projectile = projectile;
    }

    public void Shoot()
	{
        if (Time.time - _lastShootTime > _cooldown &&
			_mana.TakeMana(_projectile.ManaUse))
		{
            _lastShootTime = Time.time;

            Instantiate(_projectile.RB, _shootPoint.position, _shootPoint.rotation)
                .AddForce(_shootPoint.forward * _projectile.StartSpeed, ForceMode.VelocityChange);
        }
	}

    public void Shoot(Projectile projectile)
	{
        SetProjectile(projectile);
        Shoot();
    }
}