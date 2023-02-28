using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootForce, upwardForce;
    [SerializeField] int bulletsLeft, bulletsShot;



    [Header("Gun Stats")]
    [SerializeField] private float shootDelay;
    [SerializeField] private float gunSpread, reloadSpeed, bulletDelay;
    [SerializeField] private int magazineSize, bulletsPerShot;
    [SerializeField] private bool allowHold;


    [Header("Gun States")]
    [SerializeField]
    private bool isShooting;
    [SerializeField]
    private bool isReadyToShoot;
    [SerializeField]
    private bool isReloading;

    [Header("References")]
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform attackPoint;

    public bool allowInvoke = true;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        isReadyToShoot = true;
    }
}
