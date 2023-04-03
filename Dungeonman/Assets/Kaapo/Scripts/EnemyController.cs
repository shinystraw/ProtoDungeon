using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Enemy Statistics
    private Vector2[] pathPoints; //set of points the enemy follows, amount of points tells enemies follow distance
    public float movementSpeed; //speed at which the enemy moves
    private Vector2 idleCheckFrequency = new Vector2(1f,3f); //frequency at which enemy checks if player is visible while in idle mode
    private Vector2 pathingCheckFrequency = new Vector2(10f,20f); //frequency at which enemy checks if player is visible while chasing
    #endregion

    #region Dynamic Variables
    GameObject player; //player object

    private Vector2 modeCheckFrequency; //frequency at which enemy checks, wether mode change is needed or not
    private int movementMode;

    private float idleSpeed; //speed at which enemy moves while in idle mode
    private Vector2 idleDirection; //determines if enemy stands still or moves in positive or negative direction

    private int pointsMade; //amount of follow points made
    private int targetPoint; //current point enemy follows
    private bool playerLost; //checks if player is visible from current point
    #endregion

    void Start()
    {
        pathPoints = new Vector2[20]; //path max size, follow player only this amount of points
        movementSpeed = 0.5f; //add enemy speed
        StartCoroutine(modeCheck()); //check if mode change is ok
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //update player status
        pathPoints[0] = new Vector2(transform.position.x, transform.position.y); //keep first path point as my position

        switch (movementMode)
        {
            case 1: //in movement mode 1 enemy chases player
                Pathing();
                break;

            default: //by default enemy idle walks
                Idle();
                break;
        }
    }

    #region Mode Checking
    IEnumerator modeCheck()
    {
        yield return new WaitForSeconds(Random.Range(modeCheckFrequency.x, modeCheckFrequency.y)); //time it takes to check if mode change is needed
        idleSpeed = Random.Range(0, movementSpeed); //determine enemies idle speed at random
        idleDirection.x = Random.Range(-1, 2); //determine enemies x idle multiplier, 0 locks the axle
        idleDirection.y = Random.Range(-1, 2); //determine enemies y idle multiplier, 0 locks the axle

        RaycastHit2D collision = Physics2D.Raycast(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0]); //check player visibility from current location
        if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0 + pointsMade], player.transform.position)) //if player is seen
        {
            movementMode = 1; //start following player
        }
        else //otherwise
        {
            movementMode = 0; //go to idle mode
        }

        StartCoroutine(modeCheck()); //check if mode change is ok
    }
    #endregion

    #region Idle movement
    void Idle()
    {
        transform.position += new Vector3(idleSpeed * idleDirection.x, idleSpeed * idleDirection.y) * Time.deltaTime; //move in idle in random direction
        modeCheckFrequency = idleCheckFrequency; //change mode check frequency range
    }
    #endregion

    #region Pathing
    void Pathing()
    {
        modeCheckFrequency = pathingCheckFrequency; //change mode check frequency range

        #region Follow Player Path
        Debug.DrawRay(pathPoints[0 + pointsMade], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0 + pointsMade], Color.green); //debug enemies path when player is lost

        RaycastHit2D collision = Physics2D.Raycast(pathPoints[0 + pointsMade], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0 + pointsMade]); //check player visibility from current follow point
        if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0 + pointsMade], player.transform.position)) //if player is seen
        {
            playerLost = false; //player is seen
        }
        else //if player is not in field of view of the current point
        {
            transform.position = Vector2.MoveTowards(pathPoints[0], pathPoints[0 + pointsMade], 2 * Time.deltaTime); //move towards current path point
            if (!playerLost && pointsMade < pathPoints.Length - 1) //do only once if the max point amount is not reached
            {
                pointsMade++; //add to the point count
                playerLost = !playerLost;
            }
            pathPoints[0 + pointsMade] = collision.point + collision.normal + new Vector2(collision.normal.y * Random.Range(-0.1f, 0.1f), collision.normal.x * Random.Range(-0.1f, 0.1f)); //add this path point to the last place the player was seen
        }
        #endregion

        #region Follow Player Directly
        collision = Physics2D.Raycast(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0]); //check visibility to player
        if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0], player.transform.position)) //if player is seen
        {
            targetPoint = 0; //zero out target point
            pointsMade = 0; //zero out point list
            Debug.DrawRay(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0], Color.red); //debug enemy vision (points towards player)
            transform.position = Vector2.MoveTowards(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y), 2 * Time.deltaTime); //go towards player
        }
        #endregion

        Routing(); //after each loop go to routing
    }
    #endregion

    #region Routing
    void Routing()
    {
        if (pointsMade != 0) //if there are points to follow (player is not in field of vision)
        {
            transform.position = Vector2.MoveTowards(pathPoints[0], pathPoints[0 + targetPoint], 2 * Time.deltaTime); //move towards current target point
            if (Vector2.Distance(pathPoints[0], pathPoints[0 + targetPoint]) == 0f) //if distance to target point is sufficient
            {
                targetPoint++; //go to next target point
            }
        }

        if (targetPoint > pointsMade) //if enemy has reached the last target point, stop searching
        {
            targetPoint = 0;
            pointsMade = 0;
        }
    }
    #endregion
}
/*
Idle(); //go to idle movement

}

IEnumerator modeCheck()
{
yield return new WaitForSeconds(Random.Range(1f,10f)); //spawn enemy once timer runs out
idleDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
StartCoroutine(modeCheck()); //check if mode change is ok
}

void Idle()
{
RaycastHit2D collision = Physics2D.Raycast(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0]); //keep checking visibility to player
if (collision.collider == null || collision.distance > Vector2.Distance(pathPoints[0], player.transform.position)) //if player is seen
{
    Debug.DrawRay(pathPoints[0], new Vector2(player.transform.position.x, player.transform.position.y) - pathPoints[0], Color.blue); //debug enemy vision (points towards player)
}
else //if player is not seen
{
    currentDirection.x = idleDirection.x * 1 / (1 + Mathf.Pow(currentDirection.x, 2)*100);
    currentDirection.y = idleDirection.y * 1 / (1 + Mathf.Pow(currentDirection.y, 2)*100);
    transform.position = new Vector3(transform.position.x + currentDirection.x * Time.deltaTime, transform.position.y + currentDirection.y * Time.deltaTime, 0);
}
}
*/