using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private Animator _animator;
	[SerializeField] private float _attackDistance;
	[SerializeField] private float _damage;
	[SerializeField, HideInInspector] private NavMeshAgent _agent;
	private float _actionTimer = 0.1f;
	private HP _targetHP;

	public void SetTarget(Transform target)
	{
		_target = target;
		_targetHP = target.GetComponent<HP>();
	}

	public void Hit()
	{
		_targetHP.TakeDamage(_damage);
	}

	private void Start()
	{
		_attackDistance *= _attackDistance;

		if(_target != null)
			_targetHP = _target.GetComponent<HP>();
	}

	private void OnValidate()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		_actionTimer -= Time.deltaTime;

		if (_actionTimer < 0)
		{
			_actionTimer = Random.Range(0.1f, 0.3f);

			if (Vector3.SqrMagnitude(transform.position - _target.position) > _attackDistance)
			{
				_agent.SetDestination(_target.position);
				_animator.SetBool("Attack", false);
				_agent.isStopped = false;
			}
			else
			{
				transform.LookAt(_target);
				_animator.SetBool("Attack", true);
				_agent.isStopped = true;
			}
		}
	}
}
