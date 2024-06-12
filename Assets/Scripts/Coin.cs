using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            LevelManager.instance.IncreaseScore(1);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player2")
        {
            LevelManager.instance.IncreaseScore(2);
            gameObject.SetActive(false);
        }
    }
}
