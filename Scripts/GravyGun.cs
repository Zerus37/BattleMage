using UnityEngine;

public class GravyGun : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _maxDistance = 100;
    [SerializeField] private float _pushFore = 100;
    [SerializeField] private Transform _gravyPoint;
    [SerializeField] private Mana _mana;

    private Rigidbody _grapBody = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Grap();

        if (Input.GetMouseButtonUp(0))
            Push();
    }

	private void FixedUpdate()
	{
        if (Input.GetMouseButton(0))
            Magnite();
    }

	private void Grap()
	{
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _maxDistance, _mask))
		{
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
			{
                if (!_mana.TakeMana(10))
                    return;

                _grapBody = rb;

                _grapBody.useGravity = false;
                _grapBody.velocity = Vector3.zero;
                _grapBody.angularVelocity = Vector3.zero;
            }
        }
	}

    private void Magnite()
	{
        if (_grapBody == null)
            return;


        if (!_mana.TakeMana(3 * Time.fixedDeltaTime))
		{
            Drop();
            return;
		}

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, 2, _groundMask))
            return;

        _grapBody.transform.position = Vector3.Lerp(_grapBody.transform.position, _gravyPoint.transform.position, Time.fixedDeltaTime * 16);
    }

    private void Push()
    {
        if (_grapBody == null)
            return;


        if (!_mana.TakeMana(10))
        {
            Drop();
            return;
        }

        _grapBody.AddForce(_gravyPoint.forward * _pushFore, ForceMode.VelocityChange);

        _grapBody.useGravity = true;
        _grapBody = null;
    }

    private void Drop()
	{
        _grapBody.useGravity = true;
        _grapBody = null;
    }
}
