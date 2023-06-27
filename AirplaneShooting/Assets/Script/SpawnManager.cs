using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
public static SpawnManager instance;

    Vector3[] positions = new Vector3[5];

    public GameObject player;
    public GameObject enemy;
    public bool isSpawn;

    public float spawnDelay = 1.5f;
    float spawnTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        isSpawn = true;
        CreatePositions(); 
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 position = new Vector3(currentX, fixedY, Random.Range(minZ, maxZ));
        positions[i] = position;

        currentX += gapX;
    }
}


    // void CreatePositions()
    // {
    //     float viewPosY = 1.2f;
    //     float gapX = 1f/6f;
    //     float viewPosX = 0f;

    //     for(int i = 0; i < positions.Length; i++)
    //     {
    //         viewPosX = gapX + gapX*i;
    //         Vector3 viewPos = new Vector3(viewPosX, viewPosY, 0);
    //         Vector3 Worldpos = Camera.main.ViewportToWorldPoint(viewPos);
    //         Worldpos.z = 0f;
    //         positions[i] = Worldpos;
    //     }
    // }

    // isspawn이 true일 때 적을 랜덤하게 생성
    void spawnEnemy()
    {
        if(isSpawn == true)
        {
            if(spawnTimer > spawnDelay)
            {
                int rand = Random.Range(0, positions.Length);
                Instantiate(enemy, positions[rand], Quaternion.identity);
                spawnTimer = 0f;
            }

            spawnTimer += Time.deltaTime;
        }
        else
        {
            Debug.Log("spawnenemy else 실행");
        }
    }

    private void OnDestroy()
    {
        
            isSpawn = false;
            Debug.Log("isSpawn=false");
            //Debug.Log("Specific GameObject is destroyed!");
            // Perform any additional actions or logic specific to the destruction of the specific GameObject
        
    }
    

  

    private void Awake() 
    {
        if(SpawnManager.instance == null)
        {
            SpawnManager.instance = this;
        }
    }
}
