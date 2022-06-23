using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseDialog : MonoBehaviour
{
    public static PauseDialog Instance { get; private set; }

    bool rollStatus = false;
    bool endTurnStatus = false;

    private Button resumeButton;
    private Button quitButton;

    private void Awake()
    {
        Instance = this;
        resumeButton = transform.Find("Resume Button").GetComponent<Button>();
        quitButton = transform.Find("Quit Button").GetComponent<Button>();
        Hide();

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetRollButton(rollStatus);
            if (rollStatus == endTurnStatus && endTurnStatus == false)
            {
                endTurnStatus = true;
            }    
            GameManager.Instance.SetEndTurnButton(endTurnStatus);
            Hide();
        });

        quitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        rollStatus = GameObject.Find("Roll Button").GetComponent<Button>().interactable;
        endTurnStatus = GameObject.Find("End Turn Button").GetComponent<Button>().interactable;
        GameManager.Instance.SetRollButton(false);
        GameManager.Instance.SetEndTurnButton(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
