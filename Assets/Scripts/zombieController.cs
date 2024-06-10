using UnityEngine;

public class zombieController : MonoBehaviour
{
    public GameObject flipModel;
    public AudioClip[] idleSounds;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;

    public float runSpeed;
    public bool facingRight = true;
    bool running;
    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;
    bool Detected;
    bool firstDetection;
    float startRun;
    public float detectionTime;
    public float detectionRadius = 10f;
    public string playerTag = "Player"; // Set this to the tag assigned to player GameObjects

    GamePlayManager gameManager; // Reference to GameManager

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();
        running = false;
        Detected = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();

        // Find and store reference to GameManager
        gameManager = FindObjectOfType<GamePlayManager>();
    }

    void FixedUpdate()
    {
        FindClosestPlayer();

        if (Detected)
        {
            if (detectedPlayer != null)
            {
                if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
                else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

                if (!firstDetection)
                {
                    startRun = Time.time + detectionTime;
                    firstDetection = true;
                }
            }
        }

        if (!running && firstDetection && startRun < Time.time)
        {
            running = true;
            myAnim.SetTrigger("run");
        }

        if (running)
        {
            Vector3 moveDirection = facingRight ? Vector3.right : Vector3.left;
            transform.position += moveDirection * runSpeed * Time.fixedDeltaTime;
        }

        if (!running && Random.Range(0, 10) > 5 && nextIdleSound < Time.time)
        {
            AudioClip tempClip = idleSounds[Random.Range(0, idleSounds.Length)];
            enemyMovementAS.clip = tempClip;
            enemyMovementAS.Play();
            nextIdleSound = idleSoundTime + Time.time;
        }
    }

    private void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            Transform playerTransform = player.transform;
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestPlayer = playerTransform;
            }
        }

        if (closestPlayer != null)
        {
            Detected = true;
            detectedPlayer = closestPlayer;
            myAnim.SetBool("detected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("Detected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            firstDetection = false;
            running = false;
            Detected = false;
            detectedPlayer = null;
            myAnim.SetTrigger("idle");
        }
    }

    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.IncreasePlayerScoreForKilledZombie();
        }
    }

    void Flip()
    {
        if (flipModel != null)
        {
            facingRight = !facingRight;
            Vector3 theScale = flipModel.transform.localScale;
            theScale.z *= -1;
            flipModel.transform.localScale = theScale;
        }
    }
}
