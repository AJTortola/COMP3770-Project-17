using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public float damage, timeBetweenShooting, spread, range, impactForce, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    // [SerializeField]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;

    //Graphics
    public ParticleSystem muzzleFlash;
    public GameObject bulletHoleGraphic; // from Unity Particle Pack Asset

    public TextMeshProUGUI text;

    PhotonView PV;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        PV = GetComponentInParent<PhotonView>();
    }
    private void Update()
    {
        
        MyInput();

    }
    private void MyInput()
    {
        if (allowButtonHold) 
            shooting = Input.GetKey(KeyCode.Mouse0); // Input.GetButtonDown("Fire1"); // Shoot with left mouse button
        else 
            shooting = Input.GetKeyDown(KeyCode.Mouse0); // For sniper or semi-auto weapon which can only shoot with 1 click at a time

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) // R is reload key and you only reload
            Reload();                                                               // once you are out of bullets

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        if(PV.IsMine){

            Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

            //RayCast to do an actual shot
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range)) // Start position of array is the fps camera
            {
                Debug.Log(rayHit.transform.name); // Check if everything works fine

                if (rayHit.collider.GetComponent<PlayerMovement>()) // if hits obj with Tag "Enemy" and has the script with the TakeDamage function in it
                    rayHit.collider.GetComponent<PlayerMovement>().TakeDamage(damage);
                if (rayHit.rigidbody != null) // self-explanatory, if hit an obj with rigidbody then send it backwards once shot
                    rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }
        


            //Graphics
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
            muzzleFlash.Play();

            bulletsLeft--;
            bulletsShot--;

            Invoke("ResetShot", timeBetweenShooting);

            if(bulletsShot > 0 && bulletsLeft > 0)
                Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
