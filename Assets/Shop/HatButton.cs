using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatButton : MonoBehaviour
{

    public GameObject hat;
    public GameObject player;
    public int price;
    
    public void ClickHat()
    {

        UnityEngine.UI.Text Button = transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        if (Button.text == "Acheter" && PlayerPrefs.HasKey("Coins") && PlayerPrefs.GetInt("Coins") >= price) {
            Debug.Log("acheter");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
            if (PlayerPrefs.HasKey("HatPurshased")) {
                string skinsBrut = PlayerPrefs.GetString("HatPurshased");
                PlayerPrefs.SetString("HatPurshased", skinsBrut + "," + hat.name);
            } else {
                PlayerPrefs.SetString("HatPurshased", hat.name);
            }
            Button.text = "Activer";
        } else if (Button.text == "Activer") {
            foreach (Transform child in player.transform)
                child.gameObject.SetActive(false);
            hat.SetActive(true);
            PlayerPrefs.SetString("Hat", hat.name);
        }
    }
    public void Deactivate()
    {
        foreach (Transform child in player.transform)
            child.gameObject.SetActive(false);
        PlayerPrefs.SetString("Hat", "null");
    }
}
