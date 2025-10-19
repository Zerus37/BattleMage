using UnityEngine;

public class MagicGun : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _pushForce;
    [SerializeField] private Mana _mana;

    private float _currentCooldown = 0;

    void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) 
            && _currentCooldown <= 0 
            && _mana.TakeMana(_projectile.ManaUse))
		{
            Instantiate(_projectile.RB, _shootPoint.position, _shootPoint.rotation)
                .AddForce(_shootPoint.forward * _pushForce, ForceMode.VelocityChange);

            _currentCooldown = _cooldown;
        }
    }
}