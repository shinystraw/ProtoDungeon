using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardScript : MonoBehaviour
{
    #region Dynamic Variables
    public int keyNumber;
    #endregion

    #region Functional Variables
    SpriteRenderer rend;
    #endregion

    void Start()
    {
        #region Initializations
        rend = GetComponent<SpriteRenderer>();
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360f)); //set key card with random rotation
        #endregion

        #region Color Selection
        switch (keyNumber) //determine keycard color based on the number
        {
            case 1:
                ColorSelect(0, 1, 0); //green
                break;

            case 2:
                ColorSelect(1, 0, 0); //red
                break;

            case 3:
                ColorSelect(0, 0, 1); //blue
                break;

            case 4:
                ColorSelect(1, 1, 0); //yellow
                break;

            case 5:
                ColorSelect(1, 0, 1); //magenta
                break;

            case 6:
                ColorSelect(0, 1, 1); //cyan
                break;

            case 7:
                ColorSelect(1, 0.5f, 0); //orange
                break;

            default:
                ColorSelect(1, 1, 1); //white
                break;
        }
        #endregion
    }

    #region Card Pickup
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //get info from player
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetButton("Fire2")) //if distance to player is close enough and rmb is pressed
        {
            Destroy(gameObject); //remove keycard, it is no longer needed
            //A GUI MESSAGE CAN BE ADDED HERE
        }
    }
    #endregion

    #region Change Color
    void ColorSelect(float r, float g, float b)
    {
        rend.color = new Color(r, g, b, 1);
    }
    #endregion
}

