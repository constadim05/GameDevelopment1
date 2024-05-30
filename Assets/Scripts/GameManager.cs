using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject endgameText;
    public GameObject blackBackgroundPanel; // Reference to the black background panel
    private int playersAlive;

    void Start()
    {
        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;
        endgameText.SetActive(false);
        blackBackgroundPanel.SetActive(false); // Ensure black background panel is initially inactive
    }

    public void PlayerDied()
    {
        playersAlive--;
        if (playersAlive <= 0)
        {
            StartCoroutine(ShowEndGameText());
        }
    }

    private IEnumerator ShowEndGameText()
    {
        // Activate both endgame text and black background panel
        endgameText.SetActive(true);
        blackBackgroundPanel.SetActive(true);

        Animator endGameAnim = endgameText.GetComponent<Animator>();
        if (endGameAnim != null)
        {
            endGameAnim.SetTrigger("endGameText");
        }

        // Wait for the animation to complete
        yield return new WaitForSeconds(3f);

        // Deactivate both endgame text and black background panel
        endgameText.SetActive(false);
        blackBackgroundPanel.SetActive(false);

        // Load the main menu scene
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
