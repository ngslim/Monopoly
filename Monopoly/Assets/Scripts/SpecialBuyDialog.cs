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

    public void ShowQuestion(SpecialNode place, Action yesAction, Action noAction)
    {
        text.text = "Would you like to buy this?";
        Debug.Log(place);
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = place.nameString;
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
