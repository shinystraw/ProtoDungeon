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
    [SerializeField] Transform player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzzleFLashPrefab;
    [SerializeField] float fireRate = 0.5f;
    private float nextFire = 0;
    public float bulletForce = 250f;
    bool shoot = false;

    private void Awake()
    {
        cam = Camera.main;
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

        
    }

    private void FixedUpdate()
    {
        if (shoot)
        {
            ShootProjectile();
            shoot = false;
        }
    }

    // disables shooting while crouching
    public void DisableWhileCrouch(bool isCrouching)
    {
        gameObject.SetActive(!isCrouching);
    } 

    //logic for mouse aiming
    void MouseAiming()
    {
        transform.position = player.position;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        rotation = mousePos - (Vector2)transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void ShootProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(muzzleFLashPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
