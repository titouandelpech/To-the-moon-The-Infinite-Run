using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItem : MonoBehaviour
{
    public Image Timecount;
    void Start()
    {
        Timecount.fillAmount = 1;
    }

    void Update()
    {
        Timecount.fillAmount -= 0.2f * Time.deltaTime;
        if (Timecount.fillAmount == 0)
            Timecount.enabled = false;
    }
}
