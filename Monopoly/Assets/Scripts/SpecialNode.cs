using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialNode : Node
{
    public string owner;

    private void Start()
    {
        if (type == GameManager.NodeType.Pay)
        {
            gameObject.transform.Find("Price").GetComponent<TMP_Text>().text = (-1*GameManager.fineAmount).ToString();
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(enterPlayer);
        if(type == GameManager.NodeType.Chest)
        {
            int cardIndex = Random.Range(1, 4);
            //1: -money
            //2: +money
            //3: move forward x nodes
            if (cardIndex == 1)
            {
                int value = Random.Range(1, 4);
                value *= 100;
                if (enterPlayer.money < value)
                {
                    GameUtils.Instance.LosePlayer(enterPlayer);
                }
                else
                {
                    GameUtils.Instance.AddMoney(enterPlayer, -value);
                    MessageDialog.Instance.ShowMessage("You lost " + value.ToString(), () =>
                    {
                        GameManager.Instance.SetEndTurnButton(true);
                    });
                }
            }
            else if (cardIndex == 2)
            {
                int value = Random.Range(1, 4);
                value *= 100;
                GameUtils.Instance.AddMoney(enterPlayer, value);
                MessageDialog.Instance.ShowMessage("You are rewarded " + value.ToString(), () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
            else if (cardIndex == 3)
            {
                int steps = Random.Range(1, 13);
                MessageDialog.Instance.ShowMessage("You move " + steps.ToString() + " steps forward", () =>
                {
                    StartCoroutine(enterPlayer.Move(steps));
                });
            }

        }
        else if (type == GameManager.NodeType.Station)
        { 
            if (owner == "")
            {
                SpecialBuyDialog.Instance.ShowFactoryQuestion(this, () =>
                {
                    if (enterPlayer.money < GameManager.stationPrice)
                    {
                        MessageDialog.Instance.ShowMessage("You don't have enough money", () => { });
                    }
                    else
                    {
                        GameUtils.Instance.AddMoney(enterPlayer, -GameManager.stationPrice);
                        owner = enterPlayer.name;
                    }
                    GameManager.Instance.SetEndTurnButton(true);
                }, () =>
                {  
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
            else if (enterPlayer.name == owner)
            {
                MessageDialog.Instance.ShowMessage("You arrived at your station", () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
            else if (owner != enterPlayer.name)
            {
                GameUtils.Instance.PayStation(enterPlayer, this);
            }
        }
        else if (type == GameManager.NodeType.Pay)
        {
            if (enterPlayer.money < GameManager.fineAmount)
            {
                GameUtils.Instance.LosePlayer(enterPlayer);
            }
            else
            {
                MessageDialog.Instance.ShowMessage("You are paying a fine costing " + GameManager.fineAmount, () =>
                {
                    GameUtils.Instance.AddMoney(enterPlayer, -GameManager.fineAmount);
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }    
        }
        else if (type == GameManager.NodeType.Factory)
        {
            if (owner == "")
            {
                SpecialBuyDialog.Instance.ShowStationQuestion(this, () =>
                {
                    if (enterPlayer.money < GameManager.factoryPrice)
                    {
                        MessageDialog.Instance.ShowMessage("You don't have enough money", () => { });
                    }
                    else
                    {
                        GameUtils.Instance.AddMoney(enterPlayer, -GameManager.factoryPrice);
                        owner = enterPlayer.name;
                    }
                    GameManager.Instance.SetEndTurnButton(true);
                }, () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
            else if (enterPlayer.name == owner)
            {
                MessageDialog.Instance.ShowMessage("You arrived at your factory", () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
            else if (owner != enterPlayer.name)
            {
                GameUtils.Instance.PayFactory(enterPlayer, this);
            }
        }
        else if (type == GameManager.NodeType.Jail)
        {
            MessageDialog.Instance.ShowMessage("You are visiting the jail", () => {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
        else if (type == GameManager.NodeType.GoToJail)
        {
            GameUtils.Instance.SendToJail(enterPlayer);
        }
        else if (type == GameManager.NodeType.Park)
        {
            int lucky = Random.Range(1, 12);
            int amount = 0;
            string text = "";
            if(lucky <= 10)
            {
                text = "You are wandering around the park and found nothing";
            }
            else
            {
                amount = GameManager.parkBonus;
                text = "You are wandering around the park and found " + amount.ToString();
            }
            MessageDialog.Instance.ShowMessage(text, () => {

                GameUtils.Instance.AddMoney(enterPlayer, amount);
                GameManager.Instance.SetEndTurnButton(true);
            });
            GameManager.Instance.SetEndTurnButton(true);
        }
    }
}
