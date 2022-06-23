using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtils : MonoBehaviour
{
    public static GameUtils Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void Buy(Player player, Place place)
    {
        AddMoney(player, -place.price);
        player.places.Add(place);
        place.owner = player.transform.name;
    }

    public void Upgrade(Player player, Place place)
    {
        if (player.money < place.upgrade)
        {
            MessageDialog.Instance.ShowMessage("You don't have enough money", () => { });
            return;
        }
        AddMoney(player, -place.upgrade);
        place.houseNum++;
        if (place.houseNum == 5)
        {
            for (int i = 1; i <= 4; i++)
            {
                place.transform.Find("House " + i.ToString()).gameObject.SetActive(false);
            }
            Transform house = place.transform.Find("Villa");
            house.gameObject.SetActive(true);
            house.GetComponent<Renderer>().material.SetColor("_Color", player.GetComponent<Renderer>().material.color);
        }
        else
        {
            Transform house = place.transform.Find("House " + place.houseNum);
            house.gameObject.SetActive(true);
            house.GetComponent<Renderer>().material.SetColor("_Color", player.GetComponent<Renderer>().material.color);
        } 
    }

    public void PayPlace(Player player, Place place)
    {
        Player owner = GameObject.Find(place.owner).GetComponent<Player>();
        int payAmount = place.pay[place.houseNum];
        if (player.money < payAmount)
        {
            GameUtils.Instance.LosePlayer(player);
        }
        else
        {
            AddMoney(player, -payAmount);
            AddMoney(owner, payAmount);
            MessageDialog.Instance.ShowMessage(player.name + " paid " + payAmount + " to " + owner, () =>
            {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
    }

    public void PayStation(Player player, SpecialNode station)
    {
        int ownerStationCount = 0;
        for (int i = 1; i <= 4; i++)
        {
            SpecialNode _station = GameObject.Find("Station " + i).GetComponent<SpecialNode>();
            if (_station.owner == station.owner)
            {
                ownerStationCount++;
            }
        }
        Debug.Log(ownerStationCount);
        Debug.Log(player.GetLastStepsNum());
        int payAmount = ownerStationCount * player.GetLastStepsNum() * GameManager.stationMultiplier;
        if (player.money < payAmount)
        {
            GameUtils.Instance.LosePlayer(player);
        }
        else
        {
            Player ownerPlayer = GameObject.Find(station.owner).GetComponent<Player>();
            GameUtils.Instance.AddMoney(player, -payAmount);
            GameUtils.Instance.AddMoney(ownerPlayer, payAmount);
            MessageDialog.Instance.ShowMessage(player.name + " paid " + payAmount + " to " + station.owner, () =>
            {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
    }

    public void PayFactory(Player player, SpecialNode factory)
    {
        int payAmount = player.places.Count * GameManager.factoryMultiplier;
        Player ownerPlayer = GameObject.Find(factory.owner).GetComponent<Player>();
        if (player.money < payAmount)
        {
            GameUtils.Instance.LosePlayer(player);
        }
        else
        {
            GameUtils.Instance.AddMoney(player, -payAmount);
            GameUtils.Instance.AddMoney(ownerPlayer, payAmount);
            MessageDialog.Instance.ShowMessage(player.name + " paid " + payAmount + " to " + factory.owner, () =>
            {
                GameManager.Instance.SetEndTurnButton(true);
            });
        }
    }    

    public void AddMoney(Player player, int amount)
    {
        player.money += amount;
        Debug.Log(player.name + " " + amount.ToString());
        BudgetBoard.Instance.Refresh();
    }

    public void LosePlayer(Player player)
    {
        string playerName = player.name;
        GameObject gameObject = GameObject.Find(player.name);
        Destroy(gameObject);
        GameManager.Instance.RemovePlayer(player);
        MessageDialog.Instance.ShowMessage(playerName + " has no money to pay and lose", () =>
        {
            if (GameManager.Instance.GetPlayerNum() == 1)
            {
                MessageDialog.Instance.ShowMessage(GameManager.Instance.GetPlayer(0).name + " wins!!!", () =>
                {
                    SceneManager.LoadScene(0);
                });
            }
            else
            {
                GameManager.Instance.SetEndTurnButton(true);
            }
        });
    }    
    
    public void SendToJail(Player player)
    {
        MessageDialog.Instance.ShowMessage("You are sent to the jail", () =>
        {
            GameManager.Instance.SetEndTurnButton(true);
        });
        player.transform.position = GameManager.jailPosition;
        player.SetRoutePosition(10);
        player.inJail = true;
    }
}
