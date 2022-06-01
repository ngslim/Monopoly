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
        player.money -= place.price;
        place.owner = player.transform.name;
    }

    public void Upgrade(Player player, Place place)
    {
        player.money -= place.upgrade;
        place.houseNum++;
        Transform house = place.transform.Find("House " + place.houseNum);
        house.gameObject.SetActive(true);
        house.GetComponent<Renderer>().material.SetColor("_Color", player.GetComponent<Renderer>().material.color);
    }

    public void PayPlace(Player player, Place place)
    {
        Player owner = GameObject.Find(place.owner).GetComponent<Player>();
        int payAmount = place.pay[place.houseNum];
        player.money -= payAmount;
        owner.money += payAmount;
    }
}
