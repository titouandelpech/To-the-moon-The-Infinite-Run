using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItem : MonoBehaviour
{
    public Image Timecount;
    public Collider2D ItemCollider;
    public string Item;
    public Text ItemNb;
    public bool itemEnabled = false;
    public void Start()
    {
        Timecount.fillAmount = 1;
        ItemNb.text = PlayerPrefs.GetInt(Item).ToString();
    }

    void Update()
    {
        Timecount.fillAmount -= 0.2f * Time.deltaTime;
        checkItemUsed();
        if (Timecount.fillAmount == 0 || ItemNb.text == "0")
            gameObject.SetActive(false);
    }

    void checkItemUsed()
    {
        if (Input.touchCount > 0 && Timecount.fillAmount < 0.9)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
            {
                return;
            }
            if (ItemCollider.bounds.Contains(touch.position))
            {
                itemEnabled = true;
                PlayerPrefs.SetInt(Item, PlayerPrefs.GetInt(Item) - 1);
            }
            Timecount.fillAmount = 0;
        }
    }
}
