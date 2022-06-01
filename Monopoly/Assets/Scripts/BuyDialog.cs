using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyDialog : MonoBehaviour
{
    public static BuyDialog Instance { get; private set; }
    
    private TextMeshProUGUI text;
    private Button yesButton;
    private Button noButton;
    
    private void Awake()
    {
        Instance = this;
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesButton = transform.Find("Yes Button").GetComponent<Button>();
        noButton = transform.Find("No Button").GetComponent<Button>();

        Hide();
    }

    public void ShowQuestion(Place place, Action yesAction, Action noAction)
    {
        text.text = "Would you like to buy this?";
        Debug.Log(place);
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = place.nameString;
        transform.Find("Price").GetComponent<TextMeshProUGUI>().text = place.price.ToString();
        transform.Find("Upgrade").GetComponent<TextMeshProUGUI>().text = place.upgrade.ToString();
        for (int i = 0; i < place.pay.Length; i++)
        {
            transform.Find("Pay " + i.ToString()).GetComponent<TextMeshProUGUI>().text = place.pay[i].ToString();
        }
        gameObject.SetActive(true);
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
