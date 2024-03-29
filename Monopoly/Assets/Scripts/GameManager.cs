using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }

    public List<Player> players;
    public Route route;
    public Dice[] dices;
    public GameObject UI;
    public static int incomeEveryLap = 200;
    public static int stationPrice = 200;
    public static int stationMultiplier = 10;
    public static int factoryPrice = 200;
    public static int factoryMultiplier = 50;
    public static int fineAmount = 200;
    public static int startBudget = 2000;
    public static int jailPay = 300;
    public static int parkBonus = 200;
    public static Vector3 jailPosition = new Vector3(0.6f, 0.6f, 16f);
    public static Vector3 visitingJailPosition = new Vector3(-1f, 0f, 17.5f);
    int steps;
    int playerCount = 0;
    int currPlayer = 0;

    Button rollButton;
    Button endTurnButton;

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
        GoToJail,
    };

    bool justRolled = false;
    bool hasValues = false;
    bool hasEndedTurn = true;

    bool rollSignal = false;
    bool endTurnSignal = false;

    public void Start()
    {
        Instance = this;
        playerCount = players.Count;
        rollButton = GameObject.Find("Roll Button").GetComponent<Button>();
        endTurnButton = GameObject.Find("End Turn Button").GetComponent<Button>();
        endTurnButton.interactable = false;
        BudgetBoard.Instance.Initialize();
        rollButton.onClick.AddListener(() =>
        {
            rollSignal = true;
            SetRollButton(false);
        });

        endTurnButton.onClick.AddListener(() => {
            endTurnSignal = true;
            SetEndTurnButton(false);
            SetRollButton(true);
        });
    }

    public void Update()
    {
        if (players[currPlayer].inJail)
        {
            SetRollButton(false);
            SetEndTurnButton(false);
            QuestionDialog.Instance.ShowQuestion("You are in jail. Would you pay " + jailPay.ToString() + " to get out?", () =>
            {
                if (players[currPlayer].money < jailPay)
                {
                    MessageDialog.Instance.ShowMessage("You don't have enough money", () => { });
                    SetEndTurnButton(true);
                }
                else
                {
                    GameUtils.Instance.AddMoney(players[currPlayer], -jailPay);
                    players[currPlayer].inJail = false;
                    players[currPlayer].transform.position = visitingJailPosition + new Vector3(0, 0.6f, 0);
                    SetRollButton(true);
                }
            }, () =>
            {
                SetEndTurnButton(true);
            });
        }
        if (rollSignal && !players[currPlayer].IsMoving() && !justRolled && hasEndedTurn)
        {   
            Debug.Log(players[currPlayer].name + " is rolling");
            rollSignal = false;
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
        if(endTurnSignal && !hasEndedTurn)
        {
            endTurnSignal = false;
            Debug.Log(players[currPlayer].name + " has ended turn.");
            currPlayer = (currPlayer + 1) % playerCount;
            hasEndedTurn = true;
        }
    }

    public void EndTurn()
    {
        endTurnSignal = false;
        Debug.Log(players[currPlayer].name + " has ended turn.");
        currPlayer = (currPlayer + 1) % playerCount;
        hasEndedTurn = true;
        SetEndTurnButton(false);
        SetRollButton(true);
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

    public void RemovePlayer(Player player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i] == player)
            {
                players.RemoveAt(i);
            }
        }
        playerCount = players.Count;
        currPlayer = (currPlayer + 1) % playerCount;
    }

    public int GetPlayerNum()
    {
        return playerCount;
    }

    public void SetRollButton(bool value)
    {
        rollButton.interactable = value;
    }

    public void SetEndTurnButton(bool value)
    {
        endTurnButton.interactable = value;
    }
}
