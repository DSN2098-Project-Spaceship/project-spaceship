using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseController : MonoBehaviour
{
    //Serialized Field
    [SerializeField] Vector2 priority = new Vector2(0, 50);
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.avoidancePriority = Random.Range((int)priority.x, (int)priority.y);
        // _anim = GetComponentInChildren<Animator>();
        TryGetComponent<Animator>(out _anim);
    }

    public void GoToPosition(Vector3 position, float stoppingDistance = 0, float speed = 1)
    {
        _agent.stoppingDistance = stoppingDistance;
        _agent.speed = speed;

        _agent.SetDestination(position);

        if (_anim != null) _anim.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
    }

    public float RemainingDistance()
    {
        if (_agent.hasPath) return _agent.remainingDistance;
        return Mathf.Infinity;
    }
}
