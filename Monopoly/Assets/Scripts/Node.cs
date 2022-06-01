using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nameString; 
    public GameManager.NodeType type;
    public int i;
    protected Player enterPlayer;

    private void Start()
    {
        if (nameString == "")
        {
            if (type == GameManager.NodeType.Place)
            {
                nameString = "Unknown Place";
            } else
            {
                nameString = transform.name;
            }
        }
    }

    public virtual void OnEnter()
    {
        i = GameManager.Instance.GetCurrentPlayer();
        enterPlayer = GameManager.Instance.GetPlayer(i);
    }
}
