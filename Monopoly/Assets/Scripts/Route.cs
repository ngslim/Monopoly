using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    List<Transform> childNodeList = new List<Transform>();
    public List<Vector3> movePoints = new List<Vector3>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        FillNode();
        GetMovePoints();
        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currPos = movePoints[i];
            
            if (i > 0)
            {
                Vector3 prevPos = movePoints[i - 1];
                
                Gizmos.DrawLine(prevPos, currPos);
            }
        }        
    }

    private void GetMovePoints()
    {
        movePoints.Clear();
        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 pos = childNodeList[i].position;
            if (childNodeList[i].name.Contains("Node"))
            {
                float deg = childNodeList[i].rotation.eulerAngles.y;
                if (deg == 0) { pos.x += 0.25f; }
                if (deg == 90) { pos.z -= 0.25f; }
                if (deg == 180) { pos.x -= 0.25f; }
                if (deg == 270) { pos.z += 0.25f; }
            } else if (childNodeList[i].name == "Jail")
            {
                pos = GameManager.visitingJailPosition;
            }
            movePoints.Add(pos);
        }
    }

    public Node GetNodeInfo(int index)
    {
        FillNode();
        return childNodeList[index].GetComponent<Node>();
    }

    private void FillNode()
    {
        childNodeList.Clear();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            childNodeList.Add(this.gameObject.transform.GetChild(i));
        }
    }
}
