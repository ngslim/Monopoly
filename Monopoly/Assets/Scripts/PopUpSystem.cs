using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator popUpAnimator;
    public TMP_Text nameText;
    public TMP_Text priceText;

    public void PopUp(string name, string price)
    {
        popUpBox.SetActive(true);
        Transform nameBox = popUpBox.transform.Find("Name");
        nameText = nameBox.GetComponent<TMP_Text>();
        nameText.text = name;
        Transform priceBox = popUpBox.transform.Find("Price");
        priceText = priceBox.GetComponent<TMP_Text>();
        priceText.text = price;
    }

    public void Close()
    {
        popUpBox.SetActive(false);
    }
}
