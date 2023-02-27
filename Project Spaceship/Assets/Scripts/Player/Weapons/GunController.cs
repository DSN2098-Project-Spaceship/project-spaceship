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
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bulletTrail;
    [SerializeField] Light spot;

    //Private 
    LineRenderer lineRen;
    float nextTimeToFire;
    Recoil recoil;
    AudioSource gunSound;
    [SerializeField] bool powerOn = false;
    float currSpotIntensity;
    float maxSpotInt;
    private void Start()
    {
        recoil = FindObjectOfType<Recoil>();
        gunSound = GetComponent<AudioSource>();
        maxSpotInt = spot.intensity;
        if (!powerOn) { spot.intensity = 0; } else { currSpotIntensity = spot.intensity; }
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
        spot.intensity = Mathf.Lerp(spot.intensity, currSpotIntensity, Time.deltaTime * .45f);
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

                    lineRen = Instantiate(bulletTrail, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
                    Destroy(lineRen.gameObject, 1);

                    lineRen.SetPosition(0, shootPos.position);
                    lineRen.SetPosition(1, hit.point);
                }
                break;
        }

    }

    public void PowerOn()
    {
        powerOn = true;
        currSpotIntensity = maxSpotInt;
    }
}
