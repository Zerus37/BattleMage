using UnityEngine;

public class MagicGun : MonoBehaviour
{
    [SerializeField] private Projectile[] _projectiles;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown;
    [SerializeField] private Mana _mana;

    private float _currentCooldown = 0;
    private int _currentProjectileIndex = 0;
    
    void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) 
            && _currentCooldown <= 0 
            && _mana.TakeMana(_projectiles[_currentProjectileIndex].ManaUse))
		{
            Instantiate(_projectiles[_currentProjectileIndex].RB, _shootPoint.position, _shootPoint.rotation)
                .AddForce(_shootPoint.forward * _projectiles[_currentProjectileIndex].StartSpeed, ForceMode.VelocityChange);

            _currentCooldown = _cooldown;
        }

        if (Input.GetKeyDown(KeyCode.E))
            _currentProjectileIndex = (_currentProjectileIndex + 1) % _projectiles.Length;
    }
}