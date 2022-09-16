using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] Transform weapon;
    float recoil;
    float maxRecoil_x = -8;
    float recoilSpeed = 3;

    public void DoRecoil()
    {
        recoil += .1f;
    }

    private void Update()
    {
        Recoiling();
    }

    void Recoiling()
    {
        if (recoil > 0)
        {
            Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x + Random.Range(0f, .2f), Random.Range(0f, .2f), 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
            weapon.localEulerAngles = new Vector3(transform.localEulerAngles.x, weapon.localEulerAngles.y, weapon.localEulerAngles.z);
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0;
            Quaternion minRecoil = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, minRecoil, Time.deltaTime * recoilSpeed / 2);
            weapon.localEulerAngles = new Vector3(transform.localEulerAngles.x, weapon.localEulerAngles.y, weapon.localEulerAngles.z);
        }
    }
}
