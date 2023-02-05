using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] int myZone;
    CoverSort cs;
    public bool amActive;
    [SerializeField] List<Transform> ingressPt;
    private void Awake()
    {
        cs = FindObjectOfType<CoverSort>();
    }

    public List<Vector3> ProvidePoints()
    {
        List<Vector3> provideArray = new List<Vector3>();
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        foreach (Transform t in ingressPt) provideArray.Add(t.position);
        provideArray.Sort((x, y) => { return (playerPos - x).sqrMagnitude.CompareTo((playerPos - y).sqrMagnitude); });
        provideArray.Reverse();
        return provideArray;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cs.ChangeZone(myZone);
            DebugLogger.Log("Zone changed to " + myZone, 5);
            amActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            amActive = false;
        }
    }
}
