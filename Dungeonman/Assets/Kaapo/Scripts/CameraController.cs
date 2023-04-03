using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Camera Statistics
    public float smoothingFactor;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        smoothingFactor = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //get info from player

        #region Smooth Movement
        if (player.transform.position != transform.position)
        {
            //follow player movement with added smoothing factor
            transform.position = new Vector3(transform.position.x + (player.transform.position.x - transform.position.x)/smoothingFactor * Time.deltaTime, transform.position.y + (player.transform.position.y - transform.position.y) / smoothingFactor * Time.deltaTime, transform.position.z);
        }
        #endregion
    }
}
