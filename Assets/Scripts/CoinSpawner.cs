using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize;
    public int maxAtOnce;
    public float timeBetweenSpawns;

    public List<GameObject> coins;

    public Transform[] spawnPositionLimits;
    public 
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var coin = Instantiate(coinPrefab);
            coins.Add(coin);
            coin.SetActive(false);
        }
        InvokeRepeating("AttemptToSpawn", timeBetweenSpawns, timeBetweenSpawns);
    }

    void AttemptToSpawn()
    {
        //pick Horizontal Random
        var h = Random.Range(spawnPositionLimits[0].position.x, spawnPositionLimits[1].position.x);
        var v = Random.Range(spawnPositionLimits[3].position.z, spawnPositionLimits[0].position.z);
        var spawnVector = new Vector3(h, 0.3f, v);

        //get an innactive item from the pool
        var ObjectToSpawn = GetPooledObject();

        ObjectToSpawn.transform.position = spawnVector;
        ObjectToSpawn.SetActive(true);
    }

    GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!coins[i].activeInHierarchy)
            {
                return coins[i];
            }
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        if(spawnPositionLimits.Length > 0)
        {
            for (int i = 0; i < spawnPositionLimits.Length; i++)
            {
                if (i == spawnPositionLimits.Length - 1)
                {
                    Gizmos.DrawSphere(spawnPositionLimits[i].position, 1);
                    Gizmos.DrawLine(spawnPositionLimits[i].position, spawnPositionLimits[0].position);
                }
                else
                {
                    Gizmos.DrawSphere(spawnPositionLimits[i].position, 1);
                    Gizmos.DrawLine(spawnPositionLimits[i].position, spawnPositionLimits[i + 1].position);
                }
               
            }
        }
    }
}
