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
            GameObject platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), i), Quaternion.identity, game.transform);
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
                if (platform.gameObj.transform.position.y < -6)
                    platformToRemove = platform;
            }
        } while (platformToRemove != null);
    }

    void checkAddPlatforms()
    {
        float lastPlatPosY = Platforms[Platforms.Count - 1].gameObj.transform.position.y;
        if (lastPlatPosY < 8)
        {
            GameObject platform = Instantiate(basicPlatform, new Vector3(Random.Range(-1.7f, 1.7f), lastPlatPosY + 2), Quaternion.identity, game.transform);
            Platforms.Add(new Platform(platform));
        }
    }

    void ResetGame()
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
