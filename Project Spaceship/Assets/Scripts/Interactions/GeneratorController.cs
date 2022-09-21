using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    bool isplayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayer = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isplayer)
        {
            DebugLogger.Log("Happen Gen", 5);
            ZombieAI[] zm = FindObjectsOfType<ZombieAI>();
            FindObjectOfType<GunController>().PowerOn();
            foreach (ZombieAI z in zm)
            {
                z.PowerOn();
            }
        }
    }
}
