//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class playerHealth2 : MonoBehaviour
//{
//    public float fullHealth;
//    float currentHealth;

//    public GameObject playerDeathFX;

//    //HUD
//    public Slider playerHealthSlider;
//    public Image damageScreen;
//    Color flashColor = new Color(255f, 255f, 255f, 1f);
//    float flashSpeed = 5f;
//    bool damaged = false;
//    public GameObject endgameText;

//    AudioSource playerAS;
//    Animator endGameAnim;

//    // Start is called before the first frame update
//    void Start()
//    {
//        currentHealth = fullHealth;
//        playerHealthSlider.maxValue = fullHealth;
//        playerHealthSlider.value = currentHealth;

//        playerAS = GetComponent<AudioSource>();

//        endgameText.SetActive(false);

//        endGameAnim = endgameText.GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (damaged)
//        {
//            damageScreen.color = flashColor;
//        }
//        else
//        {
//            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, flashSpeed * Time.deltaTime);
//        }
//        damaged = false;
//    }

//    public void addDamage(float damage)
//    {
//        currentHealth -= damage;
//        playerHealthSlider.value = currentHealth;
//        damaged = true;

//        playerAS.Play();

//        if (currentHealth <= 0)
//        {
//            makeDead();
//        }
//    }

//    public void addHealth(float health)
//    {
//        currentHealth += health;
//        if (currentHealth > fullHealth) currentHealth = fullHealth;
//        playerHealthSlider.value = currentHealth;
//    }

//    public void makeDead()
//    {
//        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
//        damageScreen.color = flashColor;
//        gameObject.SetActive(false);

//        endgameText.SetActive(true);

//        endGameAnim.SetTrigger("endGame");
//    }
//}
