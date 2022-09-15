using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GlobalDamageSystem : MonoBehaviour
{
    //public
    [SerializeField] UnityEvent deathEvent;
    //Serialized Vars
    [SerializeField] int health =10;
    //private

    public void TakeDamage(int damage)
    {
        //take damage
        health -= damage;
        DebugLogger.Log("Took damage on object: " + transform.name, 4);
        if (health <= 0)
        {
            deathEvent.Invoke();
        }
    }

    public void _SimpleDeath()
    {
        Destroy(gameObject);
    }
}
