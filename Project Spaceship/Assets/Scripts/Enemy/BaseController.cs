using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseController : MonoBehaviour
{
    AIPath agent;
    Transform player;

    private void Awake()
    {
        agent = GetComponent<AIPath>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        agent.destination = player.position;
        agent.SearchPath();
    }
}
