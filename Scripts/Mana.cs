using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
	[SerializeField] private Slider _bar;
	[SerializeField] private float _maxMana;
	[SerializeField] private float _regenPerSecond;
	private float _currentMana;
	private float _burnPerSecond = 0;

	private void Start()
	{
		_bar.maxValue = _maxMana;
		_bar.value = _maxMana;
		_currentMana = _maxMana;
	}

	public bool TakeMana(float amount)
	{
		if (amount > _currentMana)
			return false;

		_currentMana -= amount;
		_bar.value = _currentMana;

		return true;
	}

	public void SetManaBurn(float value)
	{
		_burnPerSecond = value;
	}

	private void Update()
	{
		float old = _currentMana;
		_currentMana += (_regenPerSecond - _burnPerSecond) * Time.deltaTime;
		_currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);

		if (old != _currentMana)
			_bar.value = _currentMana;
	}
}