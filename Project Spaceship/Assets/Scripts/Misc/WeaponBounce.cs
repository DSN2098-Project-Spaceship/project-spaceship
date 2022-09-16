using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBounce : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time), 0) * Time.deltaTime * .2f;
        transform.eulerAngles += new Vector3(0, 20, 0) * Time.deltaTime;
    }
}
