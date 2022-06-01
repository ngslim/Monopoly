using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Place : Node
{
    public int price;
    public int houseNum = 0;
    public string owner;
    public int upgrade;
    public int[] pay;

    private void Start()
    {
        if (price == 0)
        {
            price = Random.Range(10, 40) * 10;
            upgrade = Random.Range(1, 10) * 10;
            pay = new int[6];
            pay[0] = Random.Range(1, 20) * 10;
            for (int i = 1; i < pay.Length; i++)
            {
                int temp = Random.Range(1, 20) * 10;
                pay[i] = pay[i - 1] + temp;
            }
        }
        if (nameString == "")
        {
            nameString = "Unknown Place";
        }
        transform.Find("Name").GetComponent<TMP_Text>().text = nameString;
        transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(enterPlayer);
        if (owner == "")
        {
            BuyDialog.Instance.ShowQuestion(this, () => { GameUtils.Instance.Buy(enterPlayer, this); }, () => { Debug.Log("No"); });
        }
        else if (enterPlayer.name == owner && houseNum <= 4)
        {
            QuestionDialog.Instance.ShowQuestion("Would you like to upgrade with " + upgrade.ToString() + " ?", () => { GameUtils.Instance.Upgrade(enterPlayer, this); }, () => { Debug.Log("No"); });    
        }
        else if (owner != enterPlayer.name)
        {
            GameUtils.Instance.PayPlace(enterPlayer, this);
            MessageDialog.Instance.ShowMessage(enterPlayer.name + " paid " + pay[houseNum] + " to " + owner);
        }
    }
}
