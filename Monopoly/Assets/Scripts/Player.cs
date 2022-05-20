using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route route;
    int routePosition;
    bool isMoving = false;
    int startPosition;
    public float speed = 8f;
    bool hasCompletedRoute = false;

    public bool IsMoving()
    {
        return isMoving;
    }
    
    public IEnumerator Move(int steps)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        startPosition = routePosition;
        hasCompletedRoute = false;
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
        Node dest = route.GetNodeInfo(routePosition);
        Debug.Log(dest.nameString);
        dest.OnEnter();
        hasCompletedRoute = true;
        if (routePosition < startPosition)
        {
            Debug.Log("Complete a lap");
        }
        isMoving = false;
    }

    public bool HasCompletedRoute()
    {
        return hasCompletedRoute;
    }

    public int GetRoutePosition()
    {
        return routePosition;
    }

    private bool MoveToNextNode(Vector3 dest)
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
        return dest != transform.position;
    }
}
