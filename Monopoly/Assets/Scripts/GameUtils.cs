using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        place.owner = player.transform.name;
    }

    public void Upgrade(Player player, Place place)
    {
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
        AddMoney(player, -payAmount);
        AddMoney(owner, payAmount);
    }

    public void AddMoney(Player player, int amount)
    {
        player.money += amount;
        Debug.Log(player.name + " " + amount.ToString());
        BudgetBoard.Instance.Refresh();
    }    
}
