using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageDialog : MonoBehaviour
{
    public static MessageDialog Instance { get; private set; }

    private TextMeshProUGUI text;
    private Button closeButton;

    private void Awake()
    {
        Instance = this;
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        closeButton = transform.Find("Close Button").GetComponent<Button>();
        Hide();
    }

    public void ShowMessage(string _text)
    {
        text.text = _text;
        gameObject.SetActive(true);
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
