using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
// using static UnityEngine.Random;
// using static System.Random;

public class ASPlayerAgent : Agent
{
    public GameObject player;
    public GameObject enemy;
    public GameObject bullet;

    private Rigidbody rgplayer;
    private Rigidbody rgenemy;
    private Rigidbody rgbullet;

    private const int up = 0;
    private const int down = 1;
    private const int left= 2;
    private const int right = 3;

    private Vector3 velocity;

    private Vector3 ResetPosPlayer;
    private Vector3 ResetPosEnemy;

    Vector3[] positions = new Vector3[5];
    public bool isSpawn = false;
    public float spawnDelay = 1.5f;
    float spawnTimer = 0f;
    

    public override void Initialize()
    {
        ResetPosPlayer = player.transform.position;
        ResetPosEnemy = enemy.transform.position;

        rgplayer = player.GetComponent<Rigidbody>();
        rgenemy = enemy.GetComponent<Rigidbody>();
        rgbullet = bullet.GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(player.transform.position.x);
        sensor.AddObservation(player.transform.position.z);
        sensor.AddObservation(enemy.transform.position.x);
        sensor.AddObservation(enemy.transform.position.z);
        sensor.AddObservation(rgplayer.velocity.x);
        sensor.AddObservation(rgplayer.velocity.z);
        sensor.AddObservation(rgenemy.velocity.x);
        sensor.AddObservation(rgenemy.velocity.z);
        
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var vectorAction = actionBuffers.DiscreteActions;
        Vector3 agent_pos_old = player.transform.position;
        int action = Mathf.FloorToInt(vectorAction[0]);

        switch(action)
        {
            case up:
                player.transform.position = player.transform.position + 1.0f * Vector3.down;
                break;
            case down:
                player.transform.position = player.transform.position + 1.0f * Vector3.forward;
                break;
            case left:
                player.transform.position = player.transform.position + 1.0f * Vector3.left;
                break;
            case right:
                player.transform.position = player.transform.position + 1.0f * Vector3.right;
                break;

        }

//enemy와 player가 충돌하면
        //if (collision.gameObject.CompareTag("enemy"))
        {
            AddReward(-2.0f);
            EndEpisode();
        }

       //enemy를 파괴했을 때
        //if(enemyData.getHP()<=0)
        {
            AddReward(1.0f);
            EndEpisode();
        }
        // enemy.transform.position = new Vector3
    }

    public override void OnEpisodeBegin()
    {
        player.transform.position = ResetPosPlayer;
        enemy.transform.position = ResetPosEnemy;
        rgplayer.velocity = Vector3.zero;
        rgenemy.velocity = Vector3.zero;
        CreatePositions();
        spawnEnemy();
    }

     void CreatePositions()
    {
        float fixedY = 1.5f;
        float minX = -17f;
        float maxX = 17f;
        float minZ = 10f;
        float maxZ = 36f;

        float gapX = (maxX - minX) / (positions.Length - 1);
        float currentX = minX;

        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 position = new Vector3(currentX, fixedY, UnityEngine.Random.Range(minZ, maxZ));
            positions[i] = position;

            currentX += gapX;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var DiscreteActionsOut = actionsOut.DiscreteActions;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(MoveSpeed,0,0);
            DiscreteActionsOut[0] = 3;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-MoveSpeed,0,0);
            DiscreteActionsOut[0] = 2;
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            // transform.position += new Vector3(0,0,MoveSpeed);
            DiscreteActionsOut[0] = 0;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            // transform.position += new Vector3(0,0,-MoveSpeed);
            DiscreteActionsOut[0] = 1;
        }
    }

    void spawnEnemy()
    {
        if(isSpawn == true)
        {
            if(spawnTimer > spawnDelay)
            {
                int rand = UnityEngine.Random.Range(0, positions.Length);
                Instantiate(enemy, positions[rand], Quaternion.identity);
                spawnTimer = 0f;
            }

            spawnTimer += Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
