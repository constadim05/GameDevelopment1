using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    // Start is called before the first frame update
   
        public GameObject playerPrefab;

    public float minX, maxX, minZ, maxZ;
    

    // Update is called once per frame
    void Start()
    {
        Vector3 spawnPoint = new Vector3(Random.RandomRange(minX, maxX), 0, Random.RandomRange(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity);
    }
}
