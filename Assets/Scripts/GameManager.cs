using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject endgameText;
    private int playersAlive;
    private int enemyCount;

    void Start()
    {
        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        endgameText.SetActive(false);
    }

    [PunRPC]
    public void PlayerDied()
    {
        playersAlive--;
        CheckEndGame();
    }

    [PunRPC]
    public void EnemyKilled()
    {
        enemyCount--;
        IncreasePlayerScoreForKilledZombie(); // Increase player score when enemy is killed
        CheckEndGame();
    }

    public void IncreasePlayerScoreForKilledZombie()
    {
        PlayerScoreManager playerScoreManager = FindObjectOfType<PlayerScoreManager>();
        if (playerScoreManager != null)
        {
            playerScoreManager.IncreasePlayerScore(1); // Increase player 1 score by 1
        }
    }

    private void CheckEndGame()
    {
        if (playersAlive <= 0 || enemyCount <= 0)
        {
            StartCoroutine(ShowEndGameText());
        }
    }

    private IEnumerator ShowEndGameText()
    {
        endgameText.SetActive(true);
        Animator endGameAnim = endgameText.GetComponent<Animator>();
        if (endGameAnim != null)
        {
            endGameAnim.SetTrigger("endGameText");
        }
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        LoadMainMenu();
        
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Helper method to call RPC for PlayerDied
    public void OnPlayerDied()
    {
        photonView.RPC("PlayerDied", RpcTarget.All);
    }

    // Helper method to call RPC for EnemyKilled
    public void OnEnemyKilled()
    {
        photonView.RPC("EnemyKilled", RpcTarget.All);
    }
}
