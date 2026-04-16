using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public List<NodeScript> GeneratePath(NodeScript Start, NodeScript End)
    {
        List<NodeScript> NodeList = new List<NodeScript>();

        foreach (NodeScript Node in FindObjectsOfType<NodeScript>())
        {
            Node.DScore = float.MaxValue;
        }

        Start.DScore = 0;
        Start.EndDScore = Vector2.Distance(Start.transform.position, End.transform.position);
        NodeList.Add(Start);

        while (NodeList.Count > 0)
        {
            int LowestF = default;

            for (int i = 1; i < NodeList.Count; i++)
            {
                if (NodeList[i].FinalScore() < NodeList[LowestF].FinalScore())
                {
                    LowestF = i;
                }
            }

            NodeScript CurrentNode = NodeList[LowestF];
            NodeList.Remove(CurrentNode);

            if (CurrentNode == End)
            {
                List<NodeScript> Path = new List<NodeScript>();

                Path.Insert(0,End);

                while (CurrentNode != Start)
                {
                    CurrentNode = CurrentNode.ParentNode;
                    Path.Add(CurrentNode);
                }

                Path.Reverse();
                return Path;
            }

            foreach (NodeScript ConnectedNode in CurrentNode.Connections)
            {
                float HeldGScore = CurrentNode.DScore + Vector2.Distance(CurrentNode.transform.position, ConnectedNode.transform.position);

                if (HeldGScore < ConnectedNode.DScore)
                {
                    ConnectedNode.ParentNode = CurrentNode;
                    ConnectedNode.DScore = HeldGScore;
                    ConnectedNode.EndDScore = Vector2.Distance(ConnectedNode.transform.position, End.transform.position);

                    if (!NodeList.Contains(ConnectedNode))
                    {
                        NodeList.Add(ConnectedNode);
                    }
                }    
            }
        }

        return null;
    }
}
