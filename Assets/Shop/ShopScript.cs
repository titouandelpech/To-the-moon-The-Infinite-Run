using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public GameObject [] skinList;
    public GameObject [] hatList;
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

        
        List<string> HatPurshased = PlayerPrefs.GetString("HatPurshased").Split(',').ToList();

        Debug.Log("HatPurshased: ");
        foreach (string h in HatPurshased) {
            Debug.Log(h);
        }
        
        foreach (GameObject hat in hatList) {
            Debug.Log("hat in player: " + hat.name);
            if (HatPurshased.Contains(hat.name)) {
                
                hat.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Activer";
            }
                
        }
        UpdateCoins();
    }
}
