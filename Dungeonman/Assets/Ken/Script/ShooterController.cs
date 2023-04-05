using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class ShooterController : MonoBehaviour
{
    private Camera cam;
    private Vector2 mousePos;
    private Vector2 rotation;
    Transform rotationPoint;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int ammo;
    private int currentAmmo;
    private float nextFire = 0;
    public float bulletForce = 250f;
    bool shoot = false;
    bool throwGun = false;
    Vector3 offset = new Vector3(1.5f,0,0); 

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        rotationPoint = GameObject.FindGameObjectWithTag("Equip").transform;
        rotationPoint.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.SetParent(rotationPoint);
        transform.position = rotationPoint.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        MouseAiming();

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire  = Time.time + fireRate;
            shoot = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ThrowGun();
        }
    }

    private void FixedUpdate()
    {
        if (shoot && ammo > 0)
        {
            ShootProjectile();
            ammo--;
            shoot = false;
        }
    }

    //Logiikka jolla ase pyörii hiiren suntaan
    void MouseAiming()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        rotation = mousePos - (Vector2)rotationPoint.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotationPoint.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    protected abstract void ShootProjectile();

    protected abstract void ThrowGun();

    public string CurrentAmmo()
    {
        return ammo.ToString();
    }

}
