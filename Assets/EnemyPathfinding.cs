using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public NodeScript CurrentNode;
    public List<NodeScript> Path = new List<NodeScript>();

    private void Update()
    {
        CreatePath();
    }

    public void CreatePath()
    {
        if (Path.Count > 0)
        {
            int i = 0;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Path[i].transform.position.x, Path[i].transform.position.y), 3 * Time.deltaTime);

            if (Vector2.Distance(transform.position, Path[i].transform.position) < 0.1f)
            {
                CurrentNode = Path[i];
                Path.RemoveAt(i);
            }
        }
        else
        {
            NodeScript[] Nodes = FindObjectsOfType<NodeScript>();
            while (Path == null || Path.Count == 0)
            {
                Path = AStarManager.Instance.GeneratePath(CurrentNode, Nodes[UnityEngine.Random.Range(0, Nodes.Length)]); //Start, End
            }
        }
    }
}
