using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileMove : MonoBehaviour
{
    //미사일이 날라가는 속도
    public float MoveSpeed;
    //미사일이 사라지는 시점(40)
    public float DestroyXPos;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        //매 프레임마다 미사일이  movespeed만큼 앞쪽 방향으로 날라갑니다.
        // float moveZ = MoveSpeed * Time.deltaTime;
        // transform.Translate(0,0,moveZ); 
        transform.Translate(Vector3.down * MoveSpeed );
        // 만약 미사일의 위치가 destroyYpos를 넘어서면
        if(transform.position.z >= DestroyXPos){
            Destroy(gameObject);
            GetComponent<Collider>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other) {
        // 부딛히는 collision을 가진 객체의 태그가 "enemy"일 경우
        if(other.CompareTag("enemy"))
        {
            // Debug.Log("적 기체와 충돌");
            GetComponent<Collider>().enabled = false;
        }
        
    }
}
