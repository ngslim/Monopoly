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

        ShowQuestion("Do you want to buy this?", () => { Debug.Log("Yes"); }, () => { Debug.Log("No"); });
    }

    public void ShowQuestion(string _text, Action yesAction, Action noAction)
    {
        text.text = _text;
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
