using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void GoToPosition(Vector3 position, float stoppingDistance = 0, float speed = 3)
    {
        _agent.stoppingDistance = stoppingDistance;
        _agent.speed = speed;

        _agent.SetDestination(position);
    }
}
