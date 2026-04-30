using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NodeGeneration : MonoBehaviour
{
    public NodeScript OriginNode;
    public GameObject NodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewNodes(OriginNode, OriginNode.transform.position + new Vector3(1, 0));
        GenerateNewNodes(OriginNode, OriginNode.transform.position + new Vector3(0, 1));
        GenerateNewNodes(OriginNode, OriginNode.transform.position + new Vector3(-1, 0));
        GenerateNewNodes(OriginNode, OriginNode.transform.position + new Vector3(0, -1));

        //Add Connections
        foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
        {
            if (NodeCheck.transform.position == OriginNode.transform.position + new Vector3(1, 0) || NodeCheck.transform.position == OriginNode.transform.position + new Vector3(0, 1) || NodeCheck.transform.position == OriginNode.transform.position + new Vector3(-1, 0) || NodeCheck.transform.position == OriginNode.transform.position + new Vector3(0, -1))
            {
                OriginNode.Connections.Add(NodeCheck);
                if (!NodeCheck.Connections.Contains(OriginNode))
                {
                    NodeCheck.Connections.Add(OriginNode);
                }
            }
        }
    }

    public void GenerateNewNodes(NodeScript Node, Vector3 Position)
    {
        //Get Tile On Position
        GameObject FloorObject = GameObject.FindGameObjectWithTag("Floor");
        Tilemap Floor = FloorObject.GetComponent<Tilemap>();
        Vector3Int CellPosition = Floor.WorldToCell(Position);
        Tile FloorTile = (Tile)Floor.GetTile(CellPosition);
        //Check If Position Is Taken
        bool Blocked = false;
        foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
        {
            if (NodeCheck.transform.position == Position)
            {
                Blocked = true;
            }
        }
        if (FloorTile != null && Blocked == false)
        {
            //Create New Node
            GameObject NewNodeObject = Instantiate(NodePrefab,transform);
            NodeScript NewNode = NewNodeObject.GetComponent<NodeScript>();
            NewNode.transform.position = Position;
            //Add Connections
            foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
            {
                if (NodeCheck.transform.position == Position + new Vector3(1, 0) || NodeCheck.transform.position == Position + new Vector3(0, 1) || NodeCheck.transform.position == Position + new Vector3(-1, 0) || NodeCheck.transform.position == Position + new Vector3(0, -1))
                {
                    NewNode.Connections.Add(NodeCheck);
                    if (!NodeCheck.Connections.Contains(NewNode))
                    {
                        NodeCheck.Connections.Add(NewNode);
                    }

                    //Debug.DrawLine(NodeCheck.transform.position, Position, new Color(1,0,0), 99999);
                }
            }
            //Generate More Nodes
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(1, 0));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(0, 1));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(-1, 0));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(0, -1));
        }
    }
}
