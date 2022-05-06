using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public int sideValue;
    bool onGround;

    void OnTriggerStay(Collider col)
    {
        if (col.tag=="Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground")
        {
            onGround = false;
        }
    }

    public bool IsOnGround()
    {
        return onGround;
    }
}
