using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [System.Serializable]
    public enum ZombieState
    {
        Unpowered, Chase
    }

    [SerializeField] ZombieState state = ZombieState.Unpowered;

    //private
    private BaseController _myController;
    private Transform _player;
    private Animator _animator;
    private float nextTimeToAt;
    private float atSpeed = 6.9f;

    private void Awake()
    {
        _myController = GetComponent<BaseController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();

        //Rag
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        Collider[] co = GetComponentsInChildren<Collider>();
        foreach (Rigidbody r in rb)
        {
            r.isKinematic = true;
        }
        foreach (Collider c in co)
        {
            c.isTrigger = true;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case ZombieState.Unpowered:
                Unpowered();
                break;
            case ZombieState.Chase:
                Chase();
                break;
        }
    }

    void Unpowered()
    {

    }

    void Chase()
    {
        _myController.GoToPosition(_player.position, 2);

        Collider[] nearMe = Physics.OverlapSphere(transform.position, 2.15f);
        bool pf = false;
        foreach (Collider c in nearMe)
        {
            if (c.CompareTag("Player"))
            {
                pf = true;
                if (Time.time > nextTimeToAt)
                {
                    _animator.SetBool("Attack", true);
                    nextTimeToAt = Time.time + atSpeed;
                    c.TryGetComponent<GlobalDamageSystem>(out GlobalDamageSystem gd);
                    gd.TakeDamage(3);
                }
            }
        }

        _animator.SetBool("Attack", pf);
    }

    public void PowerOn()
    {
        state = ZombieState.Chase;
    }
}