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

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();
        running = false;
        Detected = false;
        firstDetection = false;
        if (Random.Range(0, 10) > 5) Flip();
    }

    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }

        if (!running)
        {
            if (startRun < Time.time)
            {
                running = true;
                myAnim.SetTrigger("run");
            }
        }

        if (running)
        {
            // Move the zombie using transform.position
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstDetection = false;
            running = false;
            myAnim.SetTrigger("run");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}