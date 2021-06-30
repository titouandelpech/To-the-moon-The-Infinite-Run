using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    public string HatName = null;

    void Start()
    {
        HatName = PlayerPrefs.GetString("Hat");
        if (HatName == "null")
            return;
        foreach (Transform child in transform) {
            if (child.name == HatName)
                child.gameObject.SetActive(true);
        }
    }
    void Update()
    {
    }
}
