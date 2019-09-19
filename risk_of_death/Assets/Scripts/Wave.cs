using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int waveSize = 10;
    public int enemyCount = 0;
    public int spawnDelay = 5;
    void Start()
    {
        InvokeRepeating("enemyDrop", 1, spawnDelay);
    }
    void enemyDrop()
    {
        if (enemyCount % 10 == 0)
        {
            if(spawnDelay < 0)
            {
                spawnDelay = 1;
            }
            else
            {
                spawnDelay -= 1;
            }
        }
        xPos = Random.Range(1, 50);
        zPos = Random.Range(1, 31);
        Instantiate(enemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
        enemyCount += 1;
    }
}
