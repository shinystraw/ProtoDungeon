using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{

    [SerializeField] TMP_Text ammoText;
    // Start is called before the first frame update
    void Start()
    {

    }

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
