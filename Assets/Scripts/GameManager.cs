using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject endgameText;
    private int playersAlive;

    void Start()
    {
        // Set the initial count of players alive
        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;

        // Ensure endgameText is initially inactive
        endgameText.SetActive(false);
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
        // Activate the endGameText GameObject
        endgameText.SetActive(true);

        // If the endGameText GameObject has an Animator component, trigger the animation
        Animator endGameAnim = endgameText.GetComponent<Animator>();
        if (endGameAnim != null)
        {
            endGameAnim.SetTrigger("endGameText");
        }

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Load the "Main Menu" scene
        SceneManager.LoadScene("MainMenu");
    }
}