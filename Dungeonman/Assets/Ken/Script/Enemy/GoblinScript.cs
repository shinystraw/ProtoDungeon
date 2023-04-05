using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{

    [SerializeField] Transform detectionPoint;
    [SerializeField] float detectionRange;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float Speed;

    // Update is called once per frame
    void Update()
    {
        PlayerSearch();
    }

    void PlayerSearch()
    {

        Collider2D targetDetector = Physics2D.OverlapCircle(detectionPoint.position, detectionRange, playerLayer);
        if (targetDetector != null)
        {
            detectionRange = 6;
            Vector3 targetPlayer = targetDetector.gameObject.transform.position;
            Vector3 goblinPosition = gameObject.transform.position;
            Vector3 heading = targetPlayer - goblinPosition;
            Vector3 direction = heading.normalized;
            gameObject.transform.Translate(direction * Time.deltaTime * Speed);
        } 
        else
        {
            detectionRange = 2;
        }
    }


    private void OnDrawGizmos()
    {
        if (detectionPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
    }
}
