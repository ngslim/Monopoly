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
                GameUtils.Instance.AddMoney(enterPlayer, -value);
                MessageDialog.Instance.ShowMessage("You lost " + value.ToString(), () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
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
            Debug.Log("Got to station");
            if (owner == "")
            {
                SpecialBuyDialog.Instance.ShowQuestion(this, () =>
                {
                    GameUtils.Instance.AddMoney(enterPlayer, -GameManager.stationPrice);
                    owner = enterPlayer.name;
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
                int ownerStationCount = 0;
                for (int i = 1; i <=4;i++)
                {
                    SpecialNode station = GameObject.Find("Station " + i).GetComponent<SpecialNode>();
                    if (station.owner == owner)
                    {
                        ownerStationCount++;
                    }
                }
                Debug.Log(ownerStationCount);
                Debug.Log(enterPlayer.GetLastStepsNum());
                int payAmount = ownerStationCount * enterPlayer.GetLastStepsNum() * 10;
                Player ownerPlayer = GameObject.Find(owner).GetComponent<Player>();
                GameUtils.Instance.AddMoney(enterPlayer, -payAmount);
                GameUtils.Instance.AddMoney(ownerPlayer, payAmount);
                MessageDialog.Instance.ShowMessage(enterPlayer.name + " paid " + payAmount + " to " + owner, () =>
                {
                    GameManager.Instance.SetEndTurnButton(true);
                });
            }
        }
        else if (type == GameManager.NodeType.Pay)
        {
            MessageDialog.Instance.ShowMessage("You are paying a fine costing " + GameManager.fineAmount, () =>
            {
                GameUtils.Instance.AddMoney(enterPlayer, -GameManager.fineAmount);
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
        else if (type == GameManager.NodeType.Factory)
        {
            MessageDialog.Instance.ShowMessage("You are at a factory", () => {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
        else if (type == GameManager.NodeType.Jail)
        {
            MessageDialog.Instance.ShowMessage("You are visiting the jail", () => {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
        else if (type == GameManager.NodeType.GoToJail)
        {
            MessageDialog.Instance.ShowMessage("You are sent to the jail", () =>
            {
                GameManager.Instance.SetEndTurnButton(true);
            }); 
            enterPlayer.transform.position = GameObject.Find("Jail").transform.position;
        }
        else if (type == GameManager.NodeType.Park)
        {
            GameManager.Instance.SetEndTurnButton(true);
        }
    }
}
