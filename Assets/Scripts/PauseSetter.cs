using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSetter : MonoBehaviour
{
    public GameObject pause;
    public GameObject menu;
    public GameObject game;

    public PlatformsMovement PlatformsMovement;
    public Text HighScore;

    AudioSource gameMusic;
    float musicTime;

    public void goPause()
    {
        gameMusic = game.GetComponent<AudioSource>();
        musicTime = gameMusic.time;
        pause.SetActive(true);
        game.SetActive(false);
    }

    public void goBackGame()
    {
        game.SetActive(true);
        pause.SetActive(false);
        gameMusic.time = musicTime;
    }

    public void goBackMenu()
    {
        menu.SetActive(true);
        pause.SetActive(false);
        HighScore.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
        PlatformsMovement.ResetGame();
    }
}
