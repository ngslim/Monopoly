using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }

    public Player[] players;
    public Route route;
    public Dice[] dices;
    public GameObject UI;
    public static int incomeEveryLap = 200;
    int steps;
    int playerCount = 0;
    int currPlayer = 0;
    public enum NodeType
    {
        Place,
        Chest,
        Pay,
        Jail,
        Factory,
        Station,
        Start,
        Park,
    };

    bool justRolled = false;
    bool hasValues = false;
    bool hasEndedTurn = true;

    public void Start()
    {
        Instance = this;
        playerCount = players.Length;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !players[currPlayer].IsMoving() && !justRolled && hasEndedTurn)
        {
            Debug.Log(players[currPlayer].name + " is rolling");
            hasEndedTurn = false;
            justRolled = true;
            dices[0].SetValue(0);
            dices[1].SetValue(0);
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i].Roll();
            }
        }
        hasValues = true;
        for (int i = 0; i < dices.Length; i++)
        {
            if(!dices[i].HasValue())
            {
                hasValues = false;
            }
        }
        if (hasValues && justRolled)
        {
            justRolled = false;
            steps = 0;
            for (int i = 0; i < dices.Length; i++)
            {
                Debug.Log(dices[i].GetValue());
                steps += dices[i].GetValue();
            }
            StartCoroutine(players[currPlayer].Move(steps));
        }
        if(Input.GetKey(KeyCode.X) && !hasEndedTurn)
        {
            Debug.Log(players[currPlayer].name + " has ended turn.");
            currPlayer = (currPlayer + 1) % playerCount;
            hasEndedTurn = true;
        }
        

    }

    private int RollDices(int diceNum)
    {
        int sum = 0;
        for (int i = 0; i < diceNum; i++)
        {
            int diceValue = Random.Range(1, 7);
            Debug.Log("Dice " + i.ToString() + ": " + diceValue.ToString());
            sum += diceValue;
        }
        return sum;
    }

    public int GetCurrentPlayer()
    {
        return currPlayer;
    }

    public Player GetPlayer(int i)
    {
        return players[i];
    }
}
