using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGunGive : MonoBehaviour
{
    [SerializeField] GameObject gun;
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
            DebugLogger.Log("Heppen Gun", 5);
            gun.SetActive(true);
            Destroy(gameObject);
        }
    }
}
