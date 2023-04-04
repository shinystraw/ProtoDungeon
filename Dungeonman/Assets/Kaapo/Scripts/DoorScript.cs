using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    #region Instructions
    /* Sprites need to have read/write enabled
     * Renderer mode needs to be sliced
    */
    #endregion

    #region Dynamic Variables
    public Sprite[] doorSprites; //sprites of the door, base and colorful details
    private new SpriteRenderer renderer;
    public int keyNumber; //number of door, corresponds to same number keycard
    #endregion

    #region Functional Variables
    private Sprite finalSprite; //new sprite made as a combination of the base and detail sprites
    private Color detailColor; //color of the details, corresponds to keycard color
    private bool cardExists; //checks through cards, if card is found, marks it in the cardcollected variable
    private bool doorOpened; //tells if door is already opened
    private Vector2 originalLocation;
    #endregion

    void Start()
    {
        #region Initializations
        renderer = GetComponent<SpriteRenderer>(); //find the sprite renderer
        Texture2D newTexture = new Texture2D((int)doorSprites[1].rect.width, (int)doorSprites[1].rect.height); //get sprite pixel size
        Color[] pixels = new Color[newTexture.width * newTexture.height]; //Array of colors representing pixels, array size is same as sprite size
        originalLocation = transform.position; //save original position at beginning
        #endregion

        #region Detail Color Select
        switch (keyNumber) //determine keycard color based on the number
        {
            case 1:
                GetColor(0, 1, 0); //green
                break;

            case 2:
                GetColor(1, 0, 0); //red
                break;

            case 3:
                GetColor(0, 0, 1); //blue
                break;

            case 4:
                GetColor(1, 1, 0); //yellow
                break;

            case 5:
                GetColor(1, 0, 1); //magenta
                break;

            case 6:
                GetColor(0, 1, 1); //cyan
                break;

            case 7:
                GetColor(1, 0.5f, 0); //orange
                break;

            default:
                GetColor(1, 1, 1); //white
                break;
        }
        #endregion

        #region Capturing and Storing Pixels Into Texture
        for (int i = newTexture.width * newTexture.height - 1; i > 0; i--) //go through every pixel of sprite, write row by row
        {
            pixels[i] = doorSprites[0].texture.GetPixel(i - 64 * Mathf.RoundToInt(i / newTexture.width), Mathf.RoundToInt(i / newTexture.width) + 1) * detailColor; //go through all pixels of door details sprite
            if (pixels[i].a == 0) //if detail pixel has an alpha of 0
            {
                pixels[i] = doorSprites[1].texture.GetPixel(i - 64 * Mathf.RoundToInt(i / newTexture.width), Mathf.RoundToInt(i / newTexture.width) + 1); //replace pixel with door base sprite pixel
            }
        }

        newTexture.SetPixels(pixels); //set texture pixels
        newTexture.Apply(); //and apply
        #endregion

        #region Rendering Sprites
        finalSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f)); //turn newly made texture into a new sprite
        renderer.sprite = finalSprite; //add sprite to renderer
        renderer.size = new Vector2(1.92f, 0.48f); //resize to standards
        #endregion
    }

    #region Change color
    void GetColor(float r, float g, float b)
    {
        detailColor = new Color(r, g, b, 1);
    }
    #endregion

    #region Door Opening Logic
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //get info from player
        if (Vector2.Distance(transform.position, player.transform.position) < 1f) //if player is in range
        {
            GameObject[] keycards = GameObject.FindGameObjectsWithTag("Keycard"); //make array of all the existing cards
            foreach (GameObject keycard in keycards) //check every existing card
            {
                if (keycard.GetComponent<KeycardScript>().keyNumber == keyNumber) //if there is a card with same keynumber as the door
                {
                    cardExists = true; //mark card as existing
                }
            }
            if (Input.GetButton("Fire2") && cardExists == false) //if rmb is pressed and corresponding card is not found
            {
                doorOpened = true; //mark door as opened
            }
            cardExists = false; //reset card check
        }

        if (doorOpened == true) //if door is opened
        {
            OpenDoor(); //go to door opening sequence
        }
    }
    #endregion

    #region Door Opening Sequence
    void OpenDoor()
    {
        if(Vector2.Distance(transform.position, originalLocation) < renderer.size.x) //if distance to original point is not yet further than that of objects width
        {
            transform.position += transform.right * Time.deltaTime; //move to the right
        }
    }
    #endregion
}