using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] Transform pickupPoint;
    [SerializeField] float pickupRange;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] GameObject GunPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Collider2D[] hitbox = Physics2D.OverlapCircleAll(pickupPoint.position, pickupRange, itemLayer);
        foreach(Collider2D item in hitbox)
        {
            if (item.gameObject.layer == 8)
            {
                Destroy(item.gameObject);
                Instantiate(GunPrefab);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (pickupPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(pickupPoint.position, pickupRange);
    }
}
