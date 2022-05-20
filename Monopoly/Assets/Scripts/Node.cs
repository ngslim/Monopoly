using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string owner;
    public string nameString;
    public int price;
    public int houseNum;
    public GameManager.NodeType type;

    private void Start()
    {
        if (price == 0)
        {
            price = Random.Range(100, 501);
        }
        if (nameString == "")
        {
            if (type == GameManager.NodeType.Place)
            {
                nameString = "Unknown Place";
            } else
            {
                nameString = transform.name;
            }
        }
        if (type == GameManager.NodeType.Place)
        {
            transform.Find("Name").GetComponent<TextMeshPro>().text = nameString;
            transform.Find("Price").GetComponent<TextMeshPro>().text = price.ToString();
        }
    }

    public void OnEnter()
    {
        PopUpSystem popUpSystem = GameObject.Find("UI").GetComponent<PopUpSystem>();
        if (type == GameManager.NodeType.Place)
        {
            popUpSystem.PopUp(nameString, price.ToString());
        }
    }
}
