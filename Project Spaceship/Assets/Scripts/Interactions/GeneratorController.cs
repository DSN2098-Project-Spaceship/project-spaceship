using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        DebugLogger.Log("hello", 2);
        if(Input.GetKeyDown(KeyCode.F) && other.CompareTag("Player"))
        {
            ZombieAI[] zm = FindObjectsOfType<ZombieAI>();
            foreach (ZombieAI z in zm)
            {
                z.PowerOn();
            }
        }
    }
}
