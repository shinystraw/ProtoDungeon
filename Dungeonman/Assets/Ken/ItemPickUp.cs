using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] Transform pickupPoint;
    [SerializeField] float pickupRange;
    [SerializeField] LayerMask itemLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                GameObject.FindGameObjectWithTag("Gun").SetActive(true);
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
