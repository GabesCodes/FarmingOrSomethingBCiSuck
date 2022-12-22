using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;


    private float nextSpawnTime;

    [SerializeField]
    private float respawnRate;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Spawn()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);  
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + respawnRate;
        }
    }
}
