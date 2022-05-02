using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route route;
    int routePosition;
    public int steps;
    bool isMoving = false;
    public float speed = 8f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            int firstDice = Random.Range(1, 7);
            int secondDice = Random.Range(1, 7);
            steps = firstDice + secondDice;
            Debug.Log(firstDice + " " + secondDice);

            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        while(steps > 0)
        {
            routePosition++;
            routePosition %= route.movePoints.Count;
            Vector3 nextPos = route.movePoints[routePosition];
            nextPos.y += 0.6f;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;
        }
        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 dest)
    {
        return dest != (transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime));
    }
}
