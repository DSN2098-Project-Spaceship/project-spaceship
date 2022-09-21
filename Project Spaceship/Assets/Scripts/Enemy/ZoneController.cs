using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] int myZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<CoverSort>().ChangeZone(myZone);
            DebugLogger.Log("Zone changed to " + myZone, 5);
        }
    }
}
