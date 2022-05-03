using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public Player player;
    public Route route;
    public enum NodeType
    {
        Place,
        Chest,
        Pay,
        Jail,
        Factory,
        Station
    };

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !player.IsMoving())
        {
            int steps = RollDices(2);
            StartCoroutine(player.Move(steps));
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
