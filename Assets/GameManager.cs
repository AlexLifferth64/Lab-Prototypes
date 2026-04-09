using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform startBlock;
    [SerializeField] private Transform finishBlock;
    [SerializeField] private GameObject groundPrefab;

    private List<GameObject> groundList;

    private Vector2 curPos;
    private int length;
    private int height = 10;

    private float timeLeft = 8;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;

    private int level = 0;
    private bool canLoseLevel = true;

    [SerializeField] private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundList = new List<GameObject>();
        NewLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x <= finishBlock.transform.position.x + 0.75f && player.transform.position.x >= finishBlock.transform.position.x - 0.75f
                    && player.transform.position.y <= finishBlock.transform.position.y + 0.75f && player.transform.position.y >= finishBlock.transform.position.y - 0.75f)
        {
            NewLevel();
        }
        else if(player.transform.position.x <= startBlock.transform.position.x + 0.5f && player.transform.position.x >= startBlock.transform.position.x - 0.75f
                    && player.transform.position.y <= startBlock.transform.position.y + 0.5f && player.transform.position.y >= startBlock.transform.position.y - 0.75f)
        {

        }
        else
        {
            bool fellOff = true;

            foreach (GameObject o in groundList)
            {
                if(player.transform.position.x <= o.transform.position.x + 0.75f && player.transform.position.x >= o.transform.position.x - 0.75f
                    && player.transform.position.y <= o.transform.position.y + 0.75f && player.transform.position.y >= o.transform.position.y - 0.75f)
                {
                    fellOff = false;
                    canLoseLevel = true;
                    break;
                }
            }

            if(fellOff)
            {
                timeLeft -= Time.deltaTime * 6; // Time drains more quickly

                if (canLoseLevel)
                {
                    canLoseLevel = false;
                    level--;
                    if (PlayerPrefs.GetInt("HighScore") > level)
                        PlayerPrefs.SetInt("HighScore", level);

                    levelText.text = "Level: " + level;
                }
            }
        }


        timeLeft -= Time.deltaTime;

        string t = (Math.Truncate(timeLeft * 100) / 100) + "";

        if (t.Length < 4)
            t += "0";
        else if (t.Length < 3)
            t += ".00";

        timeText.text = t + "";

        if(timeLeft <= 0)
        {
            if(!gameOverText.gameObject.activeSelf)
                GameOver();
        }
    }

    private void GameOver()
    {
        if(PlayerPrefs.GetInt("HighScore") > level)
            PlayerPrefs.SetInt("HighScore", level);

        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);

        scoreText.text = "Score: " + level;
        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");

        //player.GetComponent<Movement>().enabled = false;
    }

    private void NewLevel()
    {
        foreach(GameObject o in groundList)
        {
            Destroy(o);
        }
        groundList = new List<GameObject>();

        timeLeft = 8;

        player.position = startBlock.position;

        curPos = (Vector2)startBlock.position;
        int prevRand = 5;

        int curHeight = 0;

        for(int i = 0; i < 80; i++)
        {
            int rand = UnityEngine.Random.Range(0, 3); // 0 is up, 1 is right, 2 is down

            if (i == 0)
                rand = 1;

            if(rand == 0) // up
            {
                if (curHeight < height)
                {
                    curPos += Vector2.up;
                    curHeight++;
                }
                else
                {
                    curPos += Vector2.down;
                    curHeight--;
                }
            }
            else if(rand == 1) // right
            {
                curPos += Vector2.right;
            }
            else if(rand == 2) // down
            {
                if (curHeight > -height)
                {
                    curPos += Vector2.down;
                    curHeight--;
                }
                else
                {
                    curPos += Vector2.up;
                    curHeight++;
                }
            }

            groundList.Add(Instantiate(groundPrefab, curPos, Quaternion.identity));

            prevRand = rand;
        }
        finishBlock.position = curPos + Vector2.right;


        level++;

        levelText.text = "Level: " + level;
    }
}
