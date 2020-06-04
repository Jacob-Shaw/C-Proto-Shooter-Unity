using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    [SerializeField]
    private GameObject _enemyPrefab; //Part 1 - The enemy (attach enemy prefab to the manager-this.)
    [SerializeField]
    private GameObject _enemyContainer; //Now drag enemy container into Spawn Manager
    private bool _stopSpawningEnemies = false;



    void Start()
    {
        //Part 2 - Coroutine to start the SpawnRoutine
        StartCoroutine(SpawnRoutine());
    }

    

    void Update()
    {
        

    }




    //Part 3 - Create an Ienumerator to yield events
    IEnumerator SpawnRoutine()
    {
        //yield return null; //Wait 1 frame

        while (_stopSpawningEnemies == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0); //Spawn Position 

            GameObject newEnemy = Instantiate(_enemyPrefab,positionToSpawn, Quaternion.identity); //Instatiate enemy prefab
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);  //yield wait 5 seconds
        }

        //Any code here will never happen since the above loop will never end.


    }

    public void OnPlayerDeath()
    {
        _stopSpawningEnemies = true;
    }
    
}
