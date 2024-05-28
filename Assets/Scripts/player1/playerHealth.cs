using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float fullHealth;
    float currentHealth;

    public GameObject playerDeathFX;

    //HUD
    public Slider playerHealthSlider;
    public Image damageScreen;
    Color flashColor = new Color(255f, 255f, 255f, 1f);
    float flashSpeed = 5f;
    bool damaged = false;
    public GameObject endgameText; // Changed from Text to GameObject

    AudioSource playerAS;
    Animator endGameAnim; // Animator reference for endgameText

    int playersAlive; // Counter for players that are still alive

    GameManager gameManager; // Reference to GameManager

    void Start()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;

        playerAS = GetComponent<AudioSource>();

        // Ensure endgameText is initially inactive
        endgameText.SetActive(false);

        // Get the Animator component of endgameText
        endGameAnim = endgameText.GetComponent<Animator>();

        // Set the initial count of players alive
        playersAlive = GameObject.FindGameObjectsWithTag("Player").Length;

        // Find and store reference to GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Are we hurt?
        if (damaged)
        {
            damageScreen.color = flashColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;

        playerAS.Play();

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    public void addHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        playerHealthSlider.value = currentHealth;
    }

    public void makeDead()
    {
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        gameObject.SetActive(false);

        // Decrease the count of players alive
        playersAlive--;

        if (playersAlive <= 0 && gameManager != null)
        {
            // Activate endgameText when all players are dead
            endgameText.SetActive(true);

            // Trigger the "endGame" animation
            endGameAnim.SetTrigger("endGame");

            // Notify GameManager when all players are dead
            gameManager.AllPlayersDied();
        }
    }
}
