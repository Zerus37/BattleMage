using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private Animator _animator;
	[SerializeField] private float _attackDistance;
	[SerializeField] private float _damage;
	[SerializeField] private float _runSpeed;
	[SerializeField] private float _walkSpeed;
	[SerializeField, HideInInspector] private NavMeshAgent _agent;

	[SerializeField] private UnityEvent _onAttack;
	
	private float _actionTimer = 0.1f;
	private HP _targetHP;
	private bool _pause = false;
	private bool _idle = false;

	public bool Pause => _pause;
	public Transform Target => _target;
	public Animator Animator => _animator;
	public float AttackDistance => _attackDistance;

	public void SetPause(bool flag)
	{
		_pause = flag;
		_agent.enabled = !flag;
	}

	public void SetTarget(Transform target)
	{
		_target = target;
		_targetHP = target.GetComponent<HP>();
		
		_animator.SetBool("Walk", false);
		_actionTimer = 0.05f;
		
		if (_idle)
		{
			_idle = false;
			_agent.speed = _runSpeed;
		}
	}

	public void Hit()
	{
		_targetHP.TakeDamage(_damage);
	}

	private void Start()
	{
		_attackDistance *= _attackDistance;

		if (_target != null)
		{
			_targetHP = _target.GetComponent<HP>();
			_idle = false;
			_agent.speed = _runSpeed;
		}
		else
		{
			_idle = true;
			_animator.SetBool("Walk", true);
			_agent.speed = _walkSpeed;
		}
	}

	private void OnValidate()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (_pause)
			return;

		_actionTimer -= Time.deltaTime;

		if (_actionTimer < 0)
		{
			if (!_idle)
			{
				_actionTimer = Random.Range(0.1f, 0.3f);

				if (Vector3.SqrMagnitude(transform.position - _target.position) > _attackDistance)
				{
					_agent.SetDestination(_target.position);
					_animator.SetBool("Attack", false);
				}
				else
				{
					transform.LookAt(_target);
					_animator.SetBool("Attack", true);
					if (_agent.enabled)
						_agent.ResetPath();
					_onAttack.Invoke();
				}
			}
			else
			{
				_actionTimer = Random.Range(3f, 7f);

				Vector3 walkTarget = transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
				_agent.SetDestination(walkTarget);
			}
		}
	}
}
