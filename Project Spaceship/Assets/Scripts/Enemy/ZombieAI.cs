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

        private void Awake()
        {
            _myController = GetComponent<BaseController>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            switch(state)
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
        }

        public void PowerOn()
        {
            state = EnemyState.Chase;
        }
    }
}