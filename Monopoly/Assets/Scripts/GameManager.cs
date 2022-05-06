using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players;
    public Route route;
    public Dice[] dices;
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

    public void Start()
    {
        playerCount = players.Length;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !players[currPlayer].IsMoving() && !justRolled)
        {
            Debug.Log(players[currPlayer].name + " is rolling");
            justRolled = true;
            dices[0].SetValue(0);
            dices[1].SetValue(0);
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i].Roll();
            }
        }
        if (dices[0].HasValue() && dices[1].HasValue() && justRolled)
        {
            justRolled = false;
            steps = 0;
            for (int i = 0; i < dices.Length; i++)
            {
                Debug.Log(dices[i].GetValue());
                steps += dices[i].GetValue();
            }
            StartCoroutine(players[currPlayer].Move(steps));
            currPlayer = (currPlayer + 1) % playerCount;
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

}
