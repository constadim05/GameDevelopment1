using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject endgameTextWin;   // GameObject to display when the player wins
    public GameObject endgameTextLose;  // GameObject to display when the player loses

    void Start()
    {
        // Deactivate the endgame text GameObjects at the start
        if (endgameTextWin != null) endgameTextWin.SetActive(false);
        if (endgameTextLose != null) endgameTextLose.SetActive(false);
    }

    public void ShowEndGameText(bool playerWon)
    {
        GameObject endgameText = playerWon ? endgameTextWin : endgameTextLose;

        if (endgameText != null)
        {
            endgameText.SetActive(true);
            StartCoroutine(HideEndGameText(endgameText));
        }
        else
        {
            Debug.LogError(playerWon ? "endgameTextWin is not assigned!" : "endgameTextLose is not assigned!");
        }
    }

    IEnumerator HideEndGameText(GameObject endgameText)
    {
        yield return new WaitForSeconds(3f); // Display for 3 seconds

        if (endgameText != null)
        {
            endgameText.SetActive(false);
        }
    }

    public void PlayerDied()
    {
        // Handle player death
        // For example:
        // CheckEndGame();
    }

    public void EnemyKilled()
    {
        // Handle enemy killed
        // For example:
        // CheckEndGame();
    }

    public void CheckEndGame()
    {
        // Check end game conditions
        // For example:
        // if (playersAlive <= 0) {
        //     ShowEndGameText(false);  // Player loses
        // } else if (enemyCount <= 0) {
        //     ShowEndGameText(true);  // Player wins
        // }
    }

    public void IncreasePlayerScoreForKilledZombie()
    {
        // Increase player score when enemy is killed
    }

    public void LoadMainMenu()
    {
        // Load main menu scene
    }
}
