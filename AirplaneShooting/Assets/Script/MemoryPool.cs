using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------
//메모리 풀 클래스
//특정 게임 오브젝트를 실시가능로 생서과 삭제하지 않고 
//미리 생성해 둔 게임 오브젝트를 재활용하는 클래스입니다.
//-------------------------------------------------------
//MovoBehaviour 상속 안 받음. IEnumerable상속시 foreach사용 가능
//System.IDisposable 관리되지 않는 메모리(리소스)를 해제함.

public class MemoryPool :  IEnumerable, System.IDisposable
{
    //item class
    class Item
    {
        //오브젝트가 사용하고 있는 중인지 판단하는 변수
        public bool active;
        //저장할 오브젝트
        public GameObject gameObject;
    }

    //위의 아이템 클래스를 배열로 선언(여러개의 아이템을 저장 가능)
    Item[] table;

    //열거자 기본 재정의 (foreach에서 사용하는 것인데 사용하지 않음)
    public IEnumerator GetEnumerator()
    {
        //table이 객체화되지 않으면
        if(table == null){
            //함수 탈출
            yield break;
        }

        //table이 존재하면 실행, count는 table의 길이(배열의 크기)
        int count = table.Length;

        for(int i = 0; i < count; i++)
        {
            //item에 table의 i 위치에 해당되는 객체를 대입
            Item item = table[i];
            //item이 사용중이면
            if(item.active)
            {
                //현 item의 오브젝트를 반환
                yield return item.gameObject;
            }

        }

    }

    //메모리 풀 생성
    //original: 미리 생성해 둘 원본소스
    //count : 풀 최고 갯수
    public void Create(Object original, int count)
    {
        //메모리 풀 초기화
        Dispose();
        //count만큼 배열을 생성
        table = new Item[count];

        for(int i = 0; i <count; i++)
        {
            Item item = new Item();
            item.active = false;
            //original을 GameObject형식으로 item.gameObject에 저장
            item.gameObject = GameObject.Instantiate(original) as GameObject;
            //SetActive는 활성화 함수인데 메모리에만 올릴 것이므로 비활성화 상태로 저장
            item.gameObject.SetActive(false);
            table[i] = item;
        }
        
    }

    //새 아이템 요청
    public GameObject NewItem()//GetCnumerator()와 비슷
    {
        if(table == null)
        {
            return null;
        }

        int count = table.Length;

        for(int i = 0; i < count; i++)
        {
            Item item = table[i];
            
            if(item.active == false)
            {
                item.active = true;
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }
        return null;
    }

    //아이템 사용 종료 - 사용하던 객체를 쉬게한다
    //gameObject : NewItem으로 얻었던 객체

    public void RemoveItem(GameObject gameObject)
    {
        //table이 객체화되지 않았거나, 매개 변수로 오는 gameObject가 없다면
        if(table == null || gameObject == null)
        {
            return;
        }

        //table이 존재하거나, 매개 변수로 오는 gameObject가 존재하면 실행
        //count는 table의 길이
        int count = table.Length;

        for(int i = 0; i <count; i++)
        {
            Item item = table[i];

            //매개변수 gameObject와 item으 ㅣgameObject가 같다면
            if (item.gameObject == gameObject)
            {
                item.active = false;
                item.gameObject.SetActive(false);
                break;
            }
        }
    }

    //모든 아이템 사용 종료 - 모든 객체를 쉬기 한다
    public void ClearItem()
    {
        if(table == null){
            return;
        }

        int count = table.Length;
        
        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if(item != null && item.active)
            {
                item.active = false;
                item.gameObject.SetActive(false);
            }
        }
    }

    //메모리 풀 삭제
    public void Dispose()
    {
        if(table == null){
            return;
        }

        int count = table.Length;

        for(int i = 0; i < count; i++)
        {    
            Item item = table[i];
            GameObject.Destroy(item.gameObject);
        }
        table = null;
    }

    
}
