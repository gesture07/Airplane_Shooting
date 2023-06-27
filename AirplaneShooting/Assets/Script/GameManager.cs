using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;

    int score = 0;

    public Text scoreText;

    public static GameManager instance;

    private void Awake() 
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("StartGame", 3f);//3초후 게임 실행        
    }

    void StartGame()
    {
        //player.GetComponent<Player>().canShoot = true;

        SpawnManager.instance.isSpawn = true;
    }

    public void AddScore(int enemyScore)
    {
        score += enemyScore;
        scoreText.text = "Score: "+ score;
    }

    public void MinusScore(int enemyScore)
    {
        score -= enemyScore;
        scoreText.text = "Score: "+ score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
