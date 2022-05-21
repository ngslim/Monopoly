using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Place : Node
{
    public int price;
    public int houseNum;
    public string owner;

    private void Start()
    {
        if (price == 0)
        {
            price = Random.Range(0, 500);
        }
        if (nameString == "")
        {
            nameString = "Unknown Place";
        }
        transform.Find("Name").GetComponent<TMP_Text>().text = nameString;
        transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        PopUpSystem popUpSystem = GameObject.Find("UI").GetComponent<PopUpSystem>();
        if (type == GameManager.NodeType.Place)
        {
            popUpSystem.PopUp(nameString, price.ToString());
        }
    }
}
