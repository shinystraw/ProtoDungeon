using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Statistics
    public float movementSpeed = 4; //movement speed in units per second
    public float sensitivity = 90; //sensitivity in degrees per second
    #endregion

    #region Dynamic Variables

    #endregion

    public bool movementMode;
    void Start()
    {
        
    }

    void Update()
    {
        #region Player Movements (move where body points)
        float verticalInput = Input.GetAxisRaw("Vertical"); //get vertical input
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //get horizontal input

        if (movementMode == false)
        {
            transform.position += verticalInput * movementSpeed * transform.up * Time.deltaTime; //move forward at movement speed at direction of input
            transform.Rotate(0, 0, -horizontalInput * sensitivity * Time.deltaTime); //rotate in direction of input at speed of sensitivity
        }
        #endregion

        #region Player Movements (fixed movement axis)
        if (movementMode == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //lock rotation
            //move directly in the direction of input at movementspeed
            transform.position = new Vector3(transform.position.x + movementSpeed * horizontalInput * Time.deltaTime, transform.position.y + movementSpeed * verticalInput * Time.deltaTime);
        }
        #endregion
    }
}
