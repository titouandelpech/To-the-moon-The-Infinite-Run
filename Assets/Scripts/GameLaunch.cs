using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GameLaunch : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    public Text HighScore;

    public void Start()
    {
        if (PlayerPrefs.GetInt("Already launched") == 0)
        {
            PlayerPrefs.SetInt("Already launched", 1);
            PlayerPrefs.SetInt("Apple", PlayerPrefs.GetInt("Apple") + 5);
            PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") + 5);
        }
        HighScore.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
        MobileAds.Initialize(initStatus => { });
    }
    public void LaunchGame()
    {
        menu.SetActive(false);
        game.SetActive(true);
    }
}
