using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [System.Serializable]
    enum GunType
    {
        smg
    }

    [SerializeField] GunType gunType = GunType.smg;

    //Serialized Variables
    [SerializeField] float fireRate;
    [SerializeField] int damage;

    //Serialized References
    [SerializeField] Camera mainCam;

    //Private 
    float nextTimeToFire;
    Recoil recoil;
    AudioSource gunSound;
    bool powerOn = false;
    private void Start()
    {
        recoil = FindObjectOfType<Recoil>();
        gunSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time > nextTimeToFire && Input.GetKey(KeyCode.Mouse0) && powerOn)
        {
            Shoot();
            recoil.DoRecoil();
            gunSound.Play();
            nextTimeToFire = Time.time + 1 / fireRate;
        }
    }

    void Shoot()
    {
        switch (gunType)
        {
            case GunType.smg:
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    if (hit.transform.TryGetComponent<GlobalDamageSystem>(out GlobalDamageSystem GDSystem))
                    {
                        GDSystem.TakeDamage(damage);
                    }
                }
                break;
        }

    }

    public void PowerOn()
    {
        powerOn = true;
    }
}
