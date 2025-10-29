using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	[SerializeField] private HP _hp;
	[SerializeField] private Slider _slider;

	private void Start()
	{
		_slider.maxValue = _hp.MaxHp;
		_slider.value = _hp.MaxHp;

		_hp.OnValueChange.AddListener(UpdateSlider);
	}

	public void UpdateSlider(float value)
	{
		_slider.value = value;
	}

	private void OnDisable()
	{
		_hp.OnValueChange.RemoveListener(UpdateSlider);
	}
}