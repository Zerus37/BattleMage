using UnityEngine;

public class GravyGun : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _maxDistance = 100;
    [SerializeField] private float _pushFore = 100;
    [SerializeField] private Transform _gravyPoint;
    [SerializeField] private Mana _mana;
    [SerializeField] private float _manaUse = 15;

    private Rigidbody _grapBody = null;
    private Ragdoll _grapRagdoll = null;
    private bool _work = false;

    public void Work()
	{
        if (_work)
            return;

        Grap();
        _work = true;
    }

    public void Push()
    {
        _work = false;
        if (_grapBody == null)
            return;

        if (_grapRagdoll != null)
        {
            _grapRagdoll.Push(_gravyPoint.forward * _pushFore * 2);
            _grapRagdoll.Freze(false);
            _grapRagdoll = null;
        }
        else
        {
            _grapBody.AddForce(_gravyPoint.forward * _pushFore, ForceMode.VelocityChange);
        }

        _grapBody.useGravity = true;
        _grapBody = null;
    }

    public void Drop()
    {
        if (_grapBody == null)
            return;

        _grapBody.useGravity = true;
        _grapBody = null;

        if (_grapRagdoll != null)
        {
            _grapRagdoll.Freze(false);
            _grapRagdoll = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            Drop();
    }

	private void FixedUpdate()
    {
        if (_work)
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
                if (!_mana.TakeMana(_manaUse))
                    return;

				if (hit.collider.CompareTag("Ragdoll"))
				{
                    _grapRagdoll = hit.collider.GetComponent<RagdollPart>().ragdoll;
                    if (!_grapRagdoll.Ready)
					{
                        _mana.AddMana(_manaUse);
                        return;
					}

                    _grapBody = _grapRagdoll.Root;
                    _grapRagdoll.Freze(true);
                }
				else
				{
                    _grapBody = rb;

                    _grapBody.useGravity = false;
                    _grapBody.velocity = Vector3.zero;
                    _grapBody.angularVelocity = Vector3.zero;
                }
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
}
