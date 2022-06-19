using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetBoard : MonoBehaviour
{
    public static BudgetBoard Instance { get; private set; }
    int playerCount = 0;
    private void Start()
    {
        Instance = this;
    }

    public void Refresh()
    {
        for (int i = 1; i <= playerCount; i++)
        {
            GameObject card = gameObject.transform.Find("Card " + i.ToString()).gameObject;
            TextMeshProUGUI budgetText = card.transform.Find("Budget").GetComponent<TextMeshProUGUI>();
            Player player = GameManager.Instance.GetPlayer(i - 1);
            budgetText.text = player.money.ToString();
        }
    }

    public void Initialize() 
    {
        playerCount = GameManager.Instance.GetPlayerNum();
        for (int i = playerCount + 1; i <= 4; i++)
        {
            gameObject.transform.Find("Card " + i.ToString()).gameObject.SetActive(false);
        }

        for (int i = 1; i <= playerCount; i++)
        {
            GameObject card = gameObject.transform.Find("Card " + i.ToString()).gameObject;
            TextMeshProUGUI nameText = card.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI budgetText = card.transform.Find("Budget").GetComponent<TextMeshProUGUI>();
            Player player = GameManager.Instance.GetPlayer(i - 1);
            nameText.text = player.nameString;
            nameText.color = player.GetComponent<Renderer>().material.color;
            budgetText.text = player.money.ToString();
        }
    }
}
