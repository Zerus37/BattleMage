using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
	[SerializeField] private Slider _bar;
	[SerializeField] private float _maxMana;
	[SerializeField] private float _regenPerSecond;
	private float _currentMana;

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

	private void Update()
	{
		if(_currentMana < _maxMana)
		{
			_currentMana += _regenPerSecond * Time.deltaTime;
			_bar.value = _currentMana;
		}
	}
}