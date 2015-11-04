using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker, target;
    Grid grid;

    public List<Node> path = new List<Node>();

    void Awake()
    {
        grid = GetComponent<Grid>();
    }
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        this.path = path;
        //SimplifyPath();

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
    public bool CheckIfPathIsValid(Node node)
    {
        bool walkabletemp = node.walkable;
        path = new List<Node>();
        node.walkable = false;
        FindPath(seeker.position, target.position);
        node.walkable = walkabletemp;
        //Debug.Log(path.Count);
        if (path.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }


    void OnDrawGizmos()
    {
        Node[] array = null;

        if (path != null)
        {
            array = path.ToArray();
        }

        if (array != null)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Gizmos.DrawCube(array[i].worldPosition, Vector3.one);
                Debug.DrawLine(array[i].worldPosition, array[i + 1].worldPosition);
            }
        }
        RaycastHit hit;
        Ray raym = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raym, out hit))
        {
            Node node = grid.NodeFromWorldPoint(hit.point);
            Gizmos.DrawSphere(node.worldPosition, 1); 
        }
    }
}
