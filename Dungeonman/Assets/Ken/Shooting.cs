using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera cam;
    private Vector2 mousePos;
    private Vector2 rotation;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int ammo;
    private int currentAmmo;
    private float nextFire = 0;
    public float bulletForce = 250f;
    bool shoot = false;
    bool throwGun = false;

    private void Awake()
    {
        cam = Camera.main;
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwGun == true)
        {
            return;
        }

        MouseAiming();

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire  = Time.time + fireRate;
            shoot = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = ammo;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (shoot && currentAmmo > 0)
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

    void ShootProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    //void ThrowWeapon()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    rb.AddForce(firePoint.right * bulletForce * Time.deltaTime, ForceMode2D.Impulse);
    //    Destroy(gameObject, 4f);
    //    Destroy(gameObject, 4f);
    //    // event remove from inventory the shit later.
    //}

    public string CurrentAmmo()
    {
        return ammo.ToString();
    }

}
