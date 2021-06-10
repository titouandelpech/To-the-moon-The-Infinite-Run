using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisplay : MonoBehaviour
{
    public GameObject debugJump;
    public GameObject debugMove;
    public void displayControls()
    {
        if (debugJump.activeSelf)
        {
            debugJump.SetActive(false);
            debugMove.SetActive(false);
        }
        else
        {
            debugJump.SetActive(true);
            debugMove.SetActive(true);
        }
    }
}
