using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Extensions;


public class Enemy_Data 
{
    private int hp; //적 개체의 체력
    public Enemy_Data(int _hp)// 생성자
    {
        hp = _hp;
    }

    //Enemy_Data enemy = new Enemy_Data(50); //HP가 50인 적의 데이터

    public void decreaseHP(int damage)
    {
        hp -= damage;
    }

    public int getHP()
    {
        return hp;
    }
    
}
