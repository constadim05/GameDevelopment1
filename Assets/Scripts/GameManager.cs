using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
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

    public void PlayerDied()
    {
        playersAlive--;
        CheckEndGame();
    }

    public void EnemyKilled(int playerNumber)
    {
        enemyCount--;
        IncreasePlayerScoreForKilledZombie(playerNumber); // Increase player score when enemy is killed
        CheckEndGame();
    }

    public void IncreasePlayerScoreForKilledZombie(int playerNumber)
    {
        PlayerScoreManager playerScoreManager = FindObjectOfType<PlayerScoreManager>();
        if (playerScoreManager != null)
        {
            playerScoreManager.IncreasePlayerScore(playerNumber, 1); // Increase score for the specified player
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
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Usage example method to simulate an enemy being killed by a specific player
    public void SimulateEnemyKilledByPlayer(int playerNumber)
    {
        // This method can be called to simulate an enemy kill by a specified player number
        EnemyKilled(playerNumber);
    }
}
