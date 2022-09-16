using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGunGive : MonoBehaviour
{
    [SerializeField] GameObject gun;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            gun.SetActive(true);
            Destroy(gameObject);
        }
    }
}
