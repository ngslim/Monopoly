using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialBuyDialog : MonoBehaviour
{
    public static SpecialBuyDialog Instance { get; private set; }

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

    public void ShowStationQuestion(SpecialNode place, Action yesAction, Action noAction)
    {
        text.text = "Would you like to buy this?";
        Debug.Log(place);
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = place.nameString;
        transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "Each station costs " + GameManager.stationPrice.ToString();
        transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "There are 4 stations. If other players arrive at your station after n steps, they pay:" + '\n' + "n * number of station you have * " + GameManager.stationMultiplier.ToString();
        gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
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

    public void ShowFactoryQuestion(SpecialNode place, Action yesAction, Action noAction)
    {
        text.text = "Would you like to buy this?";
        Debug.Log(place);
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = place.nameString;
        transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "Each factory costs " + GameManager.factoryPrice.ToString();
        transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "There are 2 factories. If other players arrive at your station, they pay:" + '\n' + "number of lands they have * " + GameManager.factoryMultiplier.ToString();
        gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
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
