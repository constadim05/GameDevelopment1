using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int player1Coins;
    public int player2Coins;

    public float time;

    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public TMP_Text gameStateText;
    public TMP_Text timerText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseScore(int playerNumber)
    {
        if(playerNumber == 1)
        {
            player1Coins++;
            player1Score.text = string.Format("Player {0} : {1}", 1, player1Coins.ToString());
        }
        else if (playerNumber == 2)
        {
            player2Coins++;
            player2Score.text = string.Format("Player {0} : {1}", 2, player2Coins.ToString());
        }
    }
}
