using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInhibitor : MonoBehaviour
{
    public bool movementDisabled;
    public bool shootingDisabled;

    public float GetHorizontal()
    {
        if (this.movementDisabled) return 0;
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        if (this.movementDisabled) return 0;
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetShootInput()
    {
        if (this.movementDisabled) return false;
        return Input.GetButtonDown("Fire1");
    }

    public bool GetKeyInput(KeyCode key)
    {
        if (this.movementDisabled) return false;
        return Input.GetKeyDown(key);
    }
}
