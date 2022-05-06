using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    bool hasLanded;
    bool thrown;
    Vector3 initPosition;
    public int diceValue;
    public float throwForce = 300f;

    public DiceSide[] diceSides;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            SideValueCheck();
        }
        if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollDiceAgain();
        }
    }

    public void Roll()
    {
        if (transform.position == initPosition)
        {
            RollDice();
        }
        else if (rb.IsSleeping() && hasLanded)
        {
            RollDiceAgain();
        }
    }

    private void RollDice()
    {
        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            ResetDice();
        }
    }

    private void RollDiceAgain()
    {
        ResetDice();
        thrown = true;
        rb.useGravity = true;
        rb.AddForce(Vector3.up * throwForce);
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    private void ResetDice()
    {
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    private void SideValueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in diceSides)
        {
            if (side.IsOnGround())
            {
                diceValue = side.sideValue;
            }
        }
    }

    public int GetValue()
    {
        return diceValue;
    }

    public void SetValue(int value)
    {
        diceValue = value;
    }

    public bool HasValue()
    {
        return (diceValue != 0);
    }
}
