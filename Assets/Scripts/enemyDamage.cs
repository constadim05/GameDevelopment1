using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public float damage1;
    public float damage2;
    public float damageRate1;
    public float damageRate2;
    public float pushBackForce1;
    public float pushBackForce2;

    float nextDamage1;
    float nextDamage2;

    bool player1InRange = false;
    bool player2InRange = false;

    GameObject thePlayer1;
    GameObject thePlayer2;
    playerHealth thePlayerHealth1;
    playerHealth thePlayerHealth2;

    // Start is called before the first frame update
    void Start()
    {
        nextDamage1 = Time.time;
        nextDamage2 = Time.time;

        // Find the GameObjects named "soldier_1" and "soldier_2"
        thePlayer1 = GameObject.Find("soldier_1");
        thePlayer2 = GameObject.Find("soldier_2");

        // Get the playerHealth components from the GameObjects
        if (thePlayer1 != null)
            thePlayerHealth1 = thePlayer1.GetComponent<playerHealth>();

        if (thePlayer2 != null)
            thePlayerHealth2 = thePlayer2.GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1InRange) Attack(1);
        if (player2InRange) Attack(2);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == thePlayer1)
        {
            player1InRange = true;
        }
        else if (other.gameObject == thePlayer2)
        {
            player2InRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == thePlayer1)
        {
            player1InRange = false;
        }
        else if (other.gameObject == thePlayer2)
        {
            player2InRange = false;
        }
    }

    void Attack(int playerIndex)
    {
        if (playerIndex == 1)
        {
            if (nextDamage1 <= Time.time && damage1 != null && thePlayerHealth1 != null)
            {
                thePlayerHealth1.addDamage(damage1);
                nextDamage1 = Time.time + damageRate1;
            }
        }
        else
        {
            if (nextDamage2 <= Time.time && damage2 != null && thePlayerHealth2 != null)
            {
                thePlayerHealth2.addDamage(damage2);
                nextDamage2 = Time.time + damageRate2;
            }
        }
    }
}
