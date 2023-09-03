using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(ZombieController))]
public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent _agent = null;
    private ZombieController _controller = null;
    [SerializeField] private Transform _target;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _controller = GetComponent<ZombieController>();
    }

    private void Update()
    {
        if (_target == null) return;
        if (TryAttackTarget()) return;
        MoveToTarget();
        RotateToTarget();
    }

    private void MoveToTarget()
    {
        _agent.SetDestination(_target.position);
    }

    private void RotateToTarget()
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private bool TryAttackTarget()
    {
        float distanceToTarget = Vector3.Distance(_target.position, transform.position);
        bool success = distanceToTarget <= _controller.AttackDistance;
        if (success) 
        {
            _controller.Attack(_target);
        }
        else
        {
            _controller.DropAttack();
        }
        _agent.isStopped = success;
        return success;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
