using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    #region Collision Statistics
    public float collisionDist = 0.5f; //player size
    #endregion

    #region Functional Variables
    private float colliderWidth = 0.1f;
    private float colliderHeight = 0.1f;
    private new BoxCollider2D collider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Check for Own Colliders
        if (GetComponent<BoxCollider2D>() != null) //if there is a collider
        {
            collider = GetComponent<BoxCollider2D>(); //get collider
            colliderWidth = collider.size.x/2; //get width
            colliderHeight = collider.size.y/2; //get height
            collisionDist = 0.1f; //set collision distance to be minimal
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Collision Update
        Vector2[] checkSequence = { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) }; //sequence at which the collisions are checked

        for (byte i = 0; i < 4; i++)
        {
            CollisionCheck(checkSequence[i]); //check for collisions, (0,1) for up, (0,-1) for down, (1,0) for right and (-1, 0) for left
        }
        #endregion
    }

    #region Collision Checking
    private void CollisionCheck(Vector2 sequence)
    {
        RaycastHit2D collision = Physics2D.Raycast(new Vector2(transform.position.x + colliderWidth * sequence.x, transform.position.y + colliderHeight * sequence.y), sequence); //check for collsions
        Debug.DrawRay(new Vector2(transform.position.x + colliderWidth * sequence.x, transform.position.y + colliderHeight * sequence.y), sequence);
        if (collision.collider != null && collision.distance < collisionDist && collision.collider.tag == "Solid") //if collision is detected at sane range and the collider is a solid object
        {
            transform.position = new Vector2(transform.position.x + (-collisionDist + collision.distance) * sequence.x, transform.position.y + (-collisionDist + collision.distance) * sequence.y); //push back in opposite direction
        }
    }
    #endregion
}
