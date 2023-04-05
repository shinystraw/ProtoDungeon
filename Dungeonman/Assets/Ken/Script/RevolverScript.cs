using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverScript : ShooterController
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject ThrowableObjectle;

    protected override void ShootProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    protected override void ThrowGun()
    {
        GameObject gun = Instantiate(ThrowableObjectle, gameObject.transform.position, firePoint.rotation);
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * 4000 * Time.deltaTime, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
