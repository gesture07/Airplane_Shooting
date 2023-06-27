using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Extensions;


public class Enemy : MonoBehaviour
{
    public int destroyScore = 10;
    public int minus = 20;

    public int HP;
    private Enemy_Data enemyData;
    // Start is called before the first frame update
    void Start()
    {
        enemyData = new Enemy_Data(HP);
        Debug.Log(gameObject.name + "'s HP : " + enemyData.getHP());  
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyData.getHP()<=0)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
            GameManager.instance.AddScore(destroyScore);
        } 

               
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("playerMissile"))
        {
            Debug.Log("collider missile");
            enemyData.decreaseHP(10);
            Debug.Log(gameObject.name + "'s current HP : " + enemyData.getHP());
        }
    }
}
