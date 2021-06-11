using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLaunch : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    public Text HighScore;

    public void Start()
    {
        HighScore.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
    }
    public void LaunchGame()
    {
        menu.SetActive(false);
        game.SetActive(true);
    }
}
