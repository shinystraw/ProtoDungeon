using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScript : MonoBehaviour
{
    public float mass;
    private new SpriteRenderer renderer;
    private float preferredAxis;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); //get sprite renderer from object

        if (renderer.size.x < renderer.size.y) //find out the shorter axis, and set it as the preferred axis
        {
            preferredAxis = renderer.size.x;
        }
        else
        {
            preferredAxis = renderer.size.y;
        }

        preferredAxis = preferredAxis * 3.2f; //turn to standard size 
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //get data from player
        if (Vector2.Distance(transform.position, player.transform.position) < preferredAxis) //if player is at pushing distance
        {
            //move to opposite direction to the player, determine speed by mass
            transform.position += new Vector3(System.MathF.Tanh(transform.position.x - player.transform.position.x) * 10 / mass, System.MathF.Tanh(transform.position.y - player.transform.position.y) * 10 / mass, 0) * Time.deltaTime;
            //push the player back
            player.transform.position += new Vector3(-System.MathF.Tanh(transform.position.x - player.transform.position.x) * player.GetComponent<PlayerController>().movementSpeed, -System.MathF.Tanh(transform.position.y - player.transform.position.y) * player.GetComponent<PlayerController>().movementSpeed, 0) * Time.deltaTime;
        }
    }
}
