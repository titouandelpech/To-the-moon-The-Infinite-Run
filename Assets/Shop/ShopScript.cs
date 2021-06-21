using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public GameObject [] skinList;
    public TMP_Text coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCoins() {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    private void OnEnable() {
        List<string> SkinsPurshased = PlayerPrefs.GetString("SkinsPurshased").Split(',').ToList();

        Debug.Log(SkinsPurshased);
        foreach (GameObject skin in skinList) {
            if (SkinsPurshased.Contains(skin.name))
                skin.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Activer";
        }
        UpdateCoins();
    }
}
