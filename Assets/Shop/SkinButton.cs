using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinButton : MonoBehaviour
{
    public ChangeSkin changeSkin;
    public int price;
    public string skinNbr;
    
    public void ClickSkin()
    {

        UnityEngine.UI.Text Button = transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        if (Button.text == "Acheter" && PlayerPrefs.HasKey("Coins") && PlayerPrefs.GetInt("Coins") >= price) {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
            if (PlayerPrefs.HasKey("SkinsPurshased")) {
                string skinsBrut = PlayerPrefs.GetString("SkinsPurshased");
                PlayerPrefs.SetString("SkinsPurshased", skinsBrut + "," + skinNbr);
            } else {
                PlayerPrefs.SetString("SkinsPurshased", skinNbr);
            }
            Button.text = "Activer";
        } else if (Button.text == "Activer") {
            changeSkin.SetSkinNb(int.Parse(skinNbr));
        }
    }
}
