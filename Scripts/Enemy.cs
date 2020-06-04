using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _enemySpawnHeight = 8f;
    
    Vector3 EnemyPosition;

    void Start()
    {
        RespawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //move the enemy
        transform.position = new Vector3(transform.position.x, transform.position.y - _speed * Time.deltaTime, 0);  //fix it here test play

        //If enemy hits bottom of screen, respawn at top with new random x Enemy Position
        if (transform.position.y < -5f)
        {
           RespawnEnemy();
        }        
    }


    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Hit: " + other.transform.name );

        //if other is player, damage player - destroy us 
        if (other.tag == "Player")
        {
            //Null checking to assign damage ***************************************************************************************
            Player player = other.transform.GetComponent<Player>(); //grab the component

            if (player != null) //test if the component exists before changing
            {
                player.Damage();
            }
            
        }

        
        //if other is bullet, destroy bullet destroy us
        if (other.tag == "Bullet")
        {
            Object.Destroy(other.gameObject);
        }

        Object.Destroy(this.gameObject);

    }
    

    void RespawnEnemy()
    {
        //This code will 
        float x = Random.Range(-10f, 10f);
        float y = _enemySpawnHeight;
           
        EnemyPosition = new Vector3(x, y, 0);

        transform.position = EnemyPosition;
    }

}