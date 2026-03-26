using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public NodeScript ParentNode;
    public List<NodeScript> Connections;

    public float DScore;
    public float EndDScore;

    public float FinalScore()
    {
        return DScore + EndDScore;
    }
}
