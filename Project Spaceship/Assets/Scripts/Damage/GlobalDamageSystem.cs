using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GlobalDamageSystem : MonoBehaviour
{
    //public
    [SerializeField] UnityEvent deathEvent;
    //Serialized Vars
    [SerializeField] int health = 10;
    //private
    AudioSource source;

    private void Start()
    {
        TryGetComponent<AudioSource>(out source);
    }

    public void TakeDamage(int damage)
    {
        //take damage
        health -= damage;
        if (source != null) { source.Play(); }
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

    public void RigidDeath()
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        Collider[] co = GetComponentsInChildren<Collider>();
        foreach (Rigidbody r in rb)
        {
            r.isKinematic = false;
        }
        foreach (Collider c in co)
        {
            c.isTrigger = false;
        }
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Animator>().transform.parent = null;
        Destroy(gameObject);
    }
}
