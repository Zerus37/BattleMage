using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour
{
	[SerializeField] private float _maxHp;
	[SerializeField] private UnityEvent _onDie;
	[SerializeField] private UnityEvent<float> _onValueChange;

	private float _currentHp;

	public float MaxHp => _maxHp;
	public float CurrentHp => _currentHp;
	public UnityEvent OnDie => _onDie;
	public UnityEvent<float> OnValueChange => _onValueChange;

	private void Start()
	{
		_currentHp = _maxHp;
	}

	public void TakeDamage(float amount)
	{
		_currentHp -= amount;

		_onValueChange.Invoke(_currentHp);

		if (_currentHp <= 0)
			_onDie.Invoke();
	}

	public void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
