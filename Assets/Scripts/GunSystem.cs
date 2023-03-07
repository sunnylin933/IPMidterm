using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public static GunSystem instance;
    [SerializeField] Rigidbody playerRb;

    [Header("Bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] float shootForce, upwardForce;


    [Header("Gun Stats")]
    [SerializeField] float shootDelay;
    [SerializeField] float gunSpread, reloadSpeed, bulletDelay;
    [SerializeField] public int magazineSize, bulletsPerShot;
    [SerializeField] bool allowHold;
    [SerializeField] public int bulletsLeft, bulletsFired;
    [SerializeField] float recoilForce;


    [Header("Gun States")]
    [SerializeField] bool isShooting;
    [SerializeField] bool isReadyToShoot;
    [SerializeField] bool isReloading;

    [Header("References")]
    [SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;

    public bool allowInvoke = true;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        bulletsLeft = magazineSize;
        isReadyToShoot = true;
    }

    private void Update()
    {
        ShootInput();

        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerShot + "|" + magazineSize / bulletsPerShot);
        }
    }

    private void ShootInput()
    {
        //Determines if gun is automatic
        if(allowHold)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(isReadyToShoot && isShooting && !isReloading && bulletsLeft > 0) 
        {
            bulletsFired = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        isReadyToShoot = false;

        //Casts a ray from the middle of the screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Places bullet destination at a point 5 units away on the ray
        Vector3 targetPoint = ray.GetPoint(5f);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        //Adds some randomness to bullet trajectory
        float x = Random.Range(-gunSpread, gunSpread);
        float y = Random.Range(-gunSpread, gunSpread);
        float z = Random.Range(-gunSpread, gunSpread);
        //Applies spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Aligns bullet with shoot direection
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);


        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsFired++;

        if(allowInvoke)
        {
            Invoke("ResetShot", shootDelay);
            allowInvoke = false;

            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
            print("Recoiled");
        }

        if(bulletsFired < bulletsPerShot && bulletsLeft > 0)
        {
            Invoke("Shoot", shootDelay);
        }
    }

    private void ResetShot()
    {
        isReadyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        isReloading = true; ;
        Invoke("ReloadFinished", reloadSpeed);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }
}
