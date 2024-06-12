using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster() //reacts to successfully connecting
    {
        PhotonNetwork.JoinLobby(); //try to join a lobby
    }
    public override void OnJoinedLobby() //reacts to successfully connecting to lobby
    {
        SceneManager.LoadScene("Lobby"); //loads a new scene
    }

}
