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

    private void Start()
    {
        transform.Find("Name").GetComponent<TextMeshPro>().text = nameString;
        transform.Find("Price").GetComponent<TextMeshPro>().text = price.ToString();
    }


}
