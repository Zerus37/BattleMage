using UnityEngine;

public class FlyingLightSpell : Aura
{
	[SerializeField] private SpringJoint _lightPrefab;

	private SpringJoint _light;

	private void Start()
	{
		_light = Instantiate(_lightPrefab, transform.position, Quaternion.identity);
		_light.connectedBody = _player.Rigidbody;

		_light.gameObject.SetActive(false);

		_onActivate.AddListener(() =>
		{
			_light.transform.position = transform.position + Vector3.up;
			_light.gameObject.SetActive(true);
		});
		_onDeActivate.AddListener(() => _light.gameObject.SetActive(false));
	}
}