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
    Transform player1;
    Transform player2;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();
        running = false;

        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;

        if (Random.Range(0, 10) > 5) Flip();
    }

    void FixedUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        Transform targetPlayer = GetCloserPlayer();

        if (targetPlayer != null)
        {
            if (targetPlayer.position.x < transform.position.x && facingRight)
                Flip();
            else if (targetPlayer.position.x > transform.position.x && !facingRight)
                Flip();

            running = true;
            myAnim.SetTrigger("Run");

            // Move the zombie towards the target player
            Vector3 moveDirection = (targetPlayer.position - transform.position).normalized;
            myRB.MovePosition(transform.position + moveDirection * runSpeed * Time.fixedDeltaTime);
        }
        else
        {
            running = false;
            myAnim.SetTrigger("Idle");
            myRB.velocity = Vector3.zero; // Stop the zombie if no player is detected
        }

        if (!running && Random.Range(0, 10) > 5 && nextIdleSound < Time.time)
        {
            AudioClip tempClip = idleSounds[Random.Range(0, idleSounds.Length)];
            enemyMovementAS.clip = tempClip;
            enemyMovementAS.Play();
            nextIdleSound = idleSoundTime + Time.time;
        }
    }

    Transform GetCloserPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
            return player1;
        else
            return player2;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
