using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static SpawnManager;

public class PlayerMove : MonoBehaviour
{
    //public int minus = 20;
    // private SpawnManager spawnmanager;

    public GameObject Player;
    public float MoveSpeed = 0.3f;
    public GameObject bullet;


    //public GameObject bullet; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the spawn point of the bullet
    public float bulletSpeed = 10f;

    Vector3 m_moveLimit = new Vector3(17.6f, 0, 36.2f);
    //Vector2 m_moveLimity = new Vector2(0,36.29f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        transform.localPosition = ClampPosition(transform.localPosition);
        FireBullet();
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log(("enemy충돌"));
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collider) {
        {
            if(collider.gameObject.CompareTag("enemy"))
            {
                Destroy(this.gameObject);
                // spawnmanager.isspawn = false;
                //gameObject.GetComponent<Renderer>().enabled = false;  // Make the renderer invisible//단순하게 눈에 보이지 않음. 사용하려면 bullet도 비활성화로 바꿔주는 코드를 함께 작성해서 사용해야 함.
            }
        }
    }


    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0,0,MoveSpeed);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0,0,-MoveSpeed);
        }
    }

    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
        (
            //좌우로 움직이는 이동범위 Mathf.Clamp(value, min, max)
            Mathf.Clamp(position.x, -m_moveLimit.x, m_moveLimit.x),
            0f,
            Mathf.Clamp(position.z, -m_moveLimit.z, m_moveLimit.z)
        );
    }

    
    public void FireBullet()
    {
        // Instantiate a new bullet at the bullet spawn point
        GameObject Bullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Add velocity to the bullet in the forward direction of the spawn point
        Rigidbody bulletRigidbody = Bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    

}
