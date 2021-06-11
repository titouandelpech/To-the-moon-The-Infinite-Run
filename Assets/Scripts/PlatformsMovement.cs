using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform
{
    public Platform(GameObject platform)
    {
        gameObj = platform;
    }
    public GameObject gameObj;
    public bool hasBeenPassed = false;
}

public class PlatformsMovement : MonoBehaviour
{
    public List<Platform> Platforms = new List<Platform>();
    public GameObject firstPlatform;
    public GameObject basicPlatform;
    public GameObject smallPlatform;
    public GameObject game;
    public GameObject player;

    public PlayerMovement PlayerMovement;
    public GameScore GameScore;

    public bool isPlatformsDowning;
    public float playerTempPosY;
    public float platformSpeed;
    void Start()
    {
        for (int i = -2; i < 12; i += 2)
        {
            GameObject platform = Instantiate(firstPlatform, new Vector3(Random.Range(-1.4f, 1.4f), i), Quaternion.identity, game.transform);
            Platforms.Add(new Platform(platform));
        }
    }

    void Update()
    {
        if (player.transform.position.y > -1 && PlayerMovement.isGrounded)
        {
            isPlatformsDowning = true;
            playerTempPosY = player.transform.position.y;
        }
        if (isPlatformsDowning)
        {
            MoveAllPlatformsDown();
        }
        checkCollider();
        checkDeletePlatforms();
        checkAddPlatforms();
        if (player.transform.position.y < -7)
        {
            if (PlayerPrefs.GetInt("HighScore") < GameScore.scoreValue)
                PlayerPrefs.SetInt("HighScore", GameScore.scoreValue);
            ResetGame();
        }
    }

    void MoveAllPlatformsDown()
    {
        float movement = platformSpeed * Time.deltaTime;
        foreach (Platform platform in Platforms)
        {
            platform.gameObj.transform.position -= new Vector3(0, movement);
        }
        firstPlatform.transform.position -= new Vector3(0, movement);
        player.transform.position -= new Vector3(0, movement);
        playerTempPosY -= movement;
        if (playerTempPosY < -1.1 || player.transform.position.y < -1.1)
        {
            isPlatformsDowning = false;
        }
    }

    void checkCollider()
    {
        foreach (Platform platform in Platforms)
        {
            if (player.transform.position.y + 0.1 < platform.gameObj.transform.position.y)
            {
                platform.gameObj.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                platform.gameObj.GetComponent<BoxCollider2D>().enabled = true;
                if (!platform.hasBeenPassed)
                {
                    platform.hasBeenPassed = true;
                    GameScore.scoreValue++;
                }
            }
        }
    }

    void checkDeletePlatforms()
    {
        Platform platformToRemove = null;
        do
        {
            if (platformToRemove != null)
            {
                Platforms.Remove(platformToRemove);
                Destroy(platformToRemove.gameObj);
                platformToRemove = null;
            }
            foreach (Platform platform in Platforms)
            {
                if (platform.gameObj.transform.position.y < -5.4)
                    platformToRemove = platform;
            }
        } while (platformToRemove != null);
    }

    void checkAddPlatforms()
    {
        float lastPlatPosY = Platforms[Platforms.Count - 1].gameObj.transform.position.y;
        if (lastPlatPosY < 8)
        {
            GameObject platform;
            if (GameScore.scoreValue <= 15)
                platform = Instantiate(firstPlatform, new Vector3(Random.Range(-1.4f, 1.4f), lastPlatPosY + 2), Quaternion.identity, game.transform);
            else if (GameScore.scoreValue > 15 && GameScore.scoreValue <= 40)
            {
                if (Random.Range(0, 2) == 0)
                    platform = Instantiate(firstPlatform, new Vector3(Random.Range(-1.4f, 1.4f), lastPlatPosY + 2), Quaternion.identity, game.transform);
                else
                    platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), lastPlatPosY + 2), Quaternion.identity, game.transform);
            }
            else if (GameScore.scoreValue > 40 && GameScore.scoreValue <= 90)
            {
                if (Random.Range(0, 3) == 0)
                    platform = Instantiate(firstPlatform, new Vector3(Random.Range(-1.4f, 1.4f), lastPlatPosY + 2), Quaternion.identity, game.transform);
                else if (Random.Range(0, 2) == 0)
                    platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), lastPlatPosY + 2), Quaternion.identity, game.transform);
                else
                    platform = Instantiate(smallPlatform, new Vector3(Random.Range(-1.92f, 1.92f), lastPlatPosY + 2), Quaternion.identity, game.transform);
            }
            else if (GameScore.scoreValue > 90 && GameScore.scoreValue <= 150)
            {
                if (Random.Range(0, 2) == 0)
                    platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), lastPlatPosY + 2), Quaternion.identity, game.transform);
                else
                    platform = Instantiate(smallPlatform, new Vector3(Random.Range(-1.92f, 1.92f), lastPlatPosY + 2), Quaternion.identity, game.transform);
            }
            else
            {
                if (Random.Range(0, 3) == 0)
                    platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), lastPlatPosY + 2), Quaternion.identity, game.transform);
                else
                    platform = Instantiate(smallPlatform, new Vector3(Random.Range(-1.92f, 1.92f), lastPlatPosY + 2), Quaternion.identity, game.transform);

            }
            Platforms.Add(new Platform(platform));
        }
    }

    public void ResetGame()
    {
        foreach (Platform platform in Platforms)
        {
            Destroy(platform.gameObj);
        }
        Platforms.Clear();
        firstPlatform.transform.position = new Vector3(0, -4);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.transform.position = new Vector3(0, -3.8f);
        GameScore.scoreValue = 0;
        Start();
    }
}
