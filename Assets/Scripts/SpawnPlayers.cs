using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector3 player1SpawnPoint = new Vector3(0.239999995f, 1.82000005f, -0.709999979f);
    public Vector3 player2SpawnPoint = new Vector3(7.75f, 1.82000005f, -0.709999979f);

    void Start()
    {
        // Check if this is player 1 or player 2
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, player1SpawnPoint, Quaternion.identity);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, player2SpawnPoint, Quaternion.identity);
        }
    }
}
