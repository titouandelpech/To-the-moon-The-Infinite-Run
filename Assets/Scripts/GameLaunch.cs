using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    public void LaunchGame()
    {
        menu.SetActive(false);
        game.SetActive(true);
    }
}
