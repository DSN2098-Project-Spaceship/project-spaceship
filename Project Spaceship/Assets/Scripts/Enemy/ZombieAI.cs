using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class ZombieAI : MonoBehaviour
    {
        [System.Serializable]
        public enum EnemyState
        {
            Unpowered, Chase
        }

        [SerializeField] EnemyState state = EnemyState.Unpowered;

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
        }

        private void Update()
        {
            switch (state)
            {
                case EnemyState.Unpowered:
                    Unpowered();
                    break;
                case EnemyState.Chase:
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
            foreach (Collider c in nearMe)
            {
                if (c.CompareTag("Player"))
                {
                    if (Time.time > nextTimeToAt)
                    {
                        _animator.SetTrigger("Attack");
                        nextTimeToAt = Time.time + atSpeed;
                        c.TryGetComponent<GlobalDamageSystem>(out GlobalDamageSystem gd);
                        gd.TakeDamage(3);
                    }
                }
            }
        }

        public void PowerOn()
        {
            state = EnemyState.Chase;
        }
    }
}