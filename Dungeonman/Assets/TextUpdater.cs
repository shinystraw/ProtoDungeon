using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Gun") == true)
        {
            ammoText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<ShooterController>().CurrentAmmo();
        }
        else
        {
            ammoText.text = "0";
        }
        
    }
}
