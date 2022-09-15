using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BaseController : MonoBehaviour
{
    //Serialized Field
    [SerializeField] Vector2 priority = new Vector2(0, 50);
    private NavMeshAgent _agent;
    private Transform _player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.avoidancePriority = Random.Range((int)priority.x, (int)priority.y);
    }

    public void GoToPosition(Vector3 position, float stoppingDistance = 0, float speed = 1)
    {
        _agent.stoppingDistance = stoppingDistance;
        _agent.speed = speed;

        _agent.SetDestination(position);
    }
}
