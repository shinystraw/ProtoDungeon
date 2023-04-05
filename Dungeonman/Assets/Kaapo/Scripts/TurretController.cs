using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    #region Dynamic Variables
    public float rotationAngle;
    public float turnSpeed;
    public float spinUpTime;
    public float fireRate;
    #endregion

    #region Functional Variables
    private LineRenderer lineRenderer;
    private Vector3[] vertexPositions;
    private float currentRotation;
    private float iteration;
    private Vector2 laserCheckPoint;
    private GameObject player;
    private bool mode;
    private float extraRotation;
    private bool armingLock;
    #endregion

    void Start()
    {
        extraRotation = transform.rotation.eulerAngles.z; //get extra rotation given to the turret in the editor
        lineRenderer = gameObject.AddComponent<LineRenderer>(); //add the line renderer component
        lineRenderer.startWidth = 0.05f; //set laser width
        vertexPositions = new Vector3[2]; //add two vertices, one will be start point, other end point of the laser
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //get info from player
        vertexPositions[0] = transform.position; //set first vertex to own position
        lineRenderer.SetPositions(vertexPositions); //render laser

        RaycastHit2D collision = Physics2D.Raycast(vertexPositions[0], transform.up); //check player visibility from current follow point
        if (collision.collider != null) //if collision is detected
        {
            vertexPositions[1] = collision.point; //set second point to where ever the turret is pointing at
        }

        for(int i = (int)Vector3.Distance(vertexPositions[0],vertexPositions[1]); i > 0; i--) //divide laser into equal length intervals
        {
            laserCheckPoint = vertexPositions[0] + (vertexPositions[1] - vertexPositions[0]) / Vector3.Distance(vertexPositions[0], vertexPositions[1]) * i; //an interval in laser length
            if (Vector3.Distance(laserCheckPoint, player.transform.position) < 1) //if player is in the range of the current laser interval
            {
                mode = true; //change mode to targeting
            }
            Debug.DrawRay(vertexPositions[0] + (vertexPositions[1] - vertexPositions[0])/Vector3.Distance(vertexPositions[0], vertexPositions [1]) * i, transform.right, Color.red);
        }

        if (mode == true) //if targeting player
        {
            TargetPlayer(); //target player
            if (armingLock == false)
            {
                StartCoroutine(SpinningUp());
                armingLock = true;
            }
        }
        else //otherwise
        {
            Idle(); //go to idle mode
            armingLock = false;
            StopAllCoroutines();
        }

        mode = false; //reset mode checking
        
    }

    void Idle()
    {
        transform.rotation = Quaternion.Euler(0, 0, extraRotation + currentRotation); //rotate towards the current angle, add the rotation given to turret in level editor
        iteration++; //add one to iteration, use it to make a continous curve
        currentRotation = Mathf.Sin(1f / (turnSpeed * rotationAngle) * iteration) * rotationAngle / 2; //sine curve, used for smooth turning
    }

    void TargetPlayer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position); //turn positions into rotations
        transform.rotation = Quaternion.Euler(0, 0, (System.MathF.Tanh((lookRotation.eulerAngles.y) - 180)) * lookRotation.eulerAngles.x - lookRotation.eulerAngles.y); //rotate around z axis, face towards player
        iteration = 0; //reset iteration counter
        extraRotation = transform.rotation.eulerAngles.z; //update enemy rotation at the beginning of the idle rotation
    }

    IEnumerator SpinningUp()
    {
        Debug.Log("WHIRRRR....");
        yield return new WaitForSeconds(spinUpTime); //time it takes to check if mode change is needed
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Debug.Log("Bang!");
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Shoot());
    }
}
