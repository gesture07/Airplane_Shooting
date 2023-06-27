using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{
    //복제할 미사일 오브젝트
    public GameObject PlayerMissile;
    //미사일이 발사될 위치
    public Transform MissileLocation;
    //미사일 발사 속도(미사일이 날라가는 속도x)
    public float FireDelay;
    //미사일 발사속도를 제어할 변수
    private bool FireState;

    public int MissileMaxPool;  //메모리 풀에 저장할 미사일 개수
    private MemoryPool MPool;   //메모리 풀
    private GameObject[] MissileArray;  //메모리 풀과 연동하여 사용할 미사일 배열

    //게임이 종료되면 자동으로 호출되는 함수
    private void OnApplicationQuit()
    {
        //메모리 풀을 비웁니다.
        MPool.Dispose();    
    }

    void Start()
    
    {
        //처음에 미사일을 발사할 수 있도록 제어변수를 true로 설정
        FireState = true;

        MPool = new MemoryPool();   //메모리 풀을 초기화합니다.
        MPool.Create(PlayerMissile, MissileMaxPool);    //playerMissile을 MissileMAxPool만큼 생성
        MissileArray = new GameObject[MissileMaxPool];  //배열도 초기화, 모든 값은 null

    }

    // Update is called once per frame
    void Update(){
        player_Fire();
    }

    //매 프레임마다 미사일 발사 함수를 체크한다.
    private void player_Fire(){
        //제어 변수가 true일 때먄 발동
        if(FireState){
            StartCoroutine(FireCycleControl());
            //player missile을 missileLocation의 방향으로 복제한다.
            //Instantiate(PlayerMissile, MissileLocation.position, MissileLocation.rotation);

            //미사일 풀에서 발사되지 않은 미사일을 찾아서 발사합니다.
            for(int i = 0; i < MissileMaxPool; i++)
            {
                if(MissileArray[i] == null)
                {
                    MissileArray[i] = MPool.NewItem();
                    MissileArray[i].transform.position = MissileLocation.transform.position;
                    break;
                }
            }
        }
    

        for(int i = 0; i< MissileMaxPool; i++)
        {
            if(MissileArray[i])
            {
                if(MissileArray[i].GetComponent<Collider>().enabled == false)
                {
                    MissileArray[i].GetComponent<Collider>().enabled = true;
                    MPool.RemoveItem(MissileArray[i]);
                    MissileArray[i] = null;
                }
            }
        }
    }

    //코루틴 함수 
    IEnumerator FireCycleControl(){
        //처음체 FireState를 false로 만들고
        FireState = false;
        //FireDealy초 후에 
        yield return new WaitForSeconds(FireDelay);
        //FireState를 true로 만든다.
        FireState = true;
    }
            
            
    
}
