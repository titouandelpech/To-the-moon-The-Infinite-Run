using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform
{
    public Platform(GameObject platform, GameObject coin = null, GameObject spike = null)
    {
        this.gameObj = platform;
        this.spike = spike;
        this.coin = coin;
    }
    public GameObject gameObj;
    public bool hasBeenPassed = false;
    public GameObject spike;
    public GameObject coin;
}

public class PlatformsMovement : MonoBehaviour
{
    public Animator animator;

    public List<Platform> Platforms = new List<Platform>();
    public GameObject firstPlatform;
    public GameObject basicPlatform;
    public GameObject smallPlatform;
    public GameObject basicSpike;
    public GameObject basicCoin;
    public GameObject game;
    public GameObject player;

    public PlayerMovement PlayerMovement;
    public GameScore GameScore;
    public SoundEffectHandler SoundEffectHandler;

    public bool isPlatformsDowning;
    public float playerTempPosY;
    public float platformSpeed;

    public bool isSpikeTouched;
    public float timeAfterSpiked;

    public int coinsInGame = 0;
    public Text CoinsDisplayed;

    public BonusItem AppleItem;
    public BonusItem HeartItem;
    public float timeAppleBegin = 0;

    void Start()
    {
        for (int i = -2; i < 12; i += 2)
        {
            GameObject platform = Instantiate(firstPlatform, new Vector3(Random.Range(-1.4f, 1.4f), i), Quaternion.identity, game.transform);
            GameObject coin = null;
            if (Random.Range(0, 6) == 0)
                coin = Instantiate(basicCoin, new Vector3(platform.transform.position.x, platform.transform.position.y + 0.6f), Quaternion.identity, game.transform);
            Platforms.Add(new Platform(platform, coin));
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
        checkSpikeTouched();
        checkCoinPicked();
        checkCollider();
        checkDeletePlatforms();
        checkAddPlatforms();
        moveIfApple();
        if (player.transform.position.y < -7)
        {
            SoundEffectHandler.playSound("death");
            doPlayerDied();
        }
    }

    void doPlayerDied()
    {
        if (PlayerPrefs.GetInt("HighScore") < GameScore.scoreValue)
            PlayerPrefs.SetInt("HighScore", GameScore.scoreValue);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinsInGame);
        ResetGame();
    }

    void MoveAllPlatformsDown()
    {
        float movement = platformSpeed * Time.deltaTime;
        foreach (Platform platform in Platforms)
        {
            platform.gameObj.transform.position -= new Vector3(0, movement);
            if (platform.spike)
                platform.spike.transform.position -= new Vector3(0, movement);
            if (platform.coin)
                platform.coin.transform.position -= new Vector3(0, movement);
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
            if (player.transform.position.y + 0.2 < platform.gameObj.transform.position.y)
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
                if (platformToRemove.spike)
                    Destroy(platformToRemove.spike);
                if (platformToRemove.coin)
                    Destroy(platformToRemove.coin);
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
            GameObject spike = null;
            GameObject coin = null;
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
            if (!Platforms[Platforms.Count - 1].spike && Random.Range(0, 7) == 0)
                spike = Instantiate(basicSpike, new Vector3((Random.Range(0, 2) == 0) ? platform.transform.position.x : platform.transform.position.x + 0.5f, platform.transform.position.y - 0.148f), basicSpike.transform.rotation, game.transform);
            if (Random.Range(0, 6) == 0)
                coin = Instantiate(basicCoin, new Vector3(platform.transform.position.x, platform.transform.position.y + 0.6f), Quaternion.identity, game.transform);
            Platforms.Add(new Platform(platform, coin, spike));
        }
    }

    void checkSpikeTouched()
    {
        if (isSpikeTouched)
        {
            if (Time.time - timeAfterSpiked >= 1)
            {
                doPlayerDied();
            }
            player.transform.position += new Vector3(0, -0.03f);
            player.transform.Rotate(Vector3.forward * 1);
            return;
        }
        foreach(Platform platform in Platforms)
        {
            if (platform.spike)
            {
                if (Physics2D.IsTouching(platform.spike.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>()))
                {
                    isSpikeTouched = true;
                    timeAfterSpiked = Time.time;
                    SoundEffectHandler.playSound("death");
                }
            }
        }
    }

    void checkCoinPicked()
    {
        foreach(Platform platform in Platforms)
        {
            if (platform.coin)
            {
                if (Physics2D.IsTouching(platform.coin.GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>()))
                {
                    platform.coin.GetComponent<CircleCollider2D>().enabled = false;
                    coinsInGame += 1;
                    SoundEffectHandler.playSound("coin");
                    Destroy(platform.coin);
                    platform.coin = null;
                }

            }
        }
        CoinsDisplayed.text = coinsInGame.ToString();
    }

    public void ResetGame()
    {
        //Destroy Old Platforms
        foreach (Platform platform in Platforms)
        {
            Destroy(platform.gameObj);
            if (platform.spike)
                Destroy(platform.spike);
            if (platform.coin)
                Destroy(platform.coin);
        }
        isSpikeTouched = false;
        Platforms.Clear();
        firstPlatform.transform.position = new Vector3(0, -4);

        //Reseting Player
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.transform.position = new Vector3(0, -3.8f);
        player.transform.rotation = Quaternion.identity;

        //Reseting Game Params
        GameScore.scoreValue = 0;
        coinsInGame = 0;

        //Items
        timeAppleBegin = 0;
        AppleItem.gameObject.SetActive(true);
        AppleItem.Start();
        HeartItem.gameObject.SetActive(false);
        HeartItem.Start();
        Start();
    }

    public void moveIfApple()
    {
        if (AppleItem.itemEnabled)
        {
            if (timeAppleBegin == 0)
            {
                timeAppleBegin = Time.time;
                player.GetComponent<BoxCollider2D>().enabled = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                animator.SetBool("isApple", true);
            }
            platformSpeed = 7;
            isPlatformsDowning = true;
            player.transform.position += new Vector3(0, 8f, 0) * Time.deltaTime;
            if (Time.time - timeAppleBegin >= 6)
            {
                player.GetComponent<BoxCollider2D>().enabled = true;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                AppleItem.itemEnabled = false;
                isPlatformsDowning = false;
                platformSpeed = 3;
                animator.SetBool("isApple", false);
            }
        }
    }
}
