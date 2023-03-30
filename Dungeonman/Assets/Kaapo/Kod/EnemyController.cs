using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //comment
    #region Enemy Statistics
    private Vector2[] pathPoints;
    private int[] stepRandomness = new int[10];
    public int pointsMade;
    private int targetPoint;
    private bool targetLock;
    private bool playerLost;
    #endregion

    #region Dynamic Variables
    GameObject player;
    #endregion

    void Start()
    {
        stepRandomness[0] = 1;
        pathPoints = new Vector2[100]; //make path max size
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Pathing();
    }

    void Pathing()
    {
        pathPoints[0] = new Vector2(transform.position.x, transform.position.y);
        Debug.DrawRay(pathPoints[0 + pointsMade], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0 + pointsMade], Color.green);

        RaycastHit2D collision = Physics2D.Raycast(pathPoints[0+pointsMade], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0+pointsMade]);
        if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0+pointsMade], player.transform.position))
        {
            playerLost = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(pathPoints[0], pathPoints[0 + pointsMade], 2 * Time.deltaTime);
            if (!playerLost)
            {
                pointsMade++;
                playerLost = !playerLost;
            }
            pathPoints[0 + pointsMade] = collision.point + collision.normal + new Vector2(collision.normal.y * Random.Range(-1f, 1f), collision.normal.x * Random.Range(-1f, 1f));
        }

        collision = Physics2D.Raycast(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0]);
        if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0], player.transform.position))
        {
            targetPoint = 0;
            pointsMade = 0;
            Debug.DrawRay(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0], Color.red);
            transform.position = Vector2.MoveTowards(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y), 2 * Time.deltaTime);
        }
        Routing();
    }
    void Routing()
    {
        if (pointsMade != 0)
        {
            transform.position = Vector2.MoveTowards(pathPoints[0], pathPoints[0+targetPoint], 2 * Time.deltaTime);
            if (Vector2.Distance(pathPoints[0], pathPoints[0 + targetPoint]) < 1f)
            {
                Debug.Log(targetPoint);
                targetPoint++;
            }
        }

        if (targetPoint > pointsMade)
        {
            targetPoint = 0;
            pointsMade = 0;
        }
    }
}
