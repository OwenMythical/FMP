using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    }

    public void GenerateNewNodes(NodeScript Node, Vector3 Position)
    {
        //Get Tile On Position
        Tilemap Floor = FindObjectOfType<Tilemap>();
        Vector3Int CellPosition = Floor.WorldToCell(Position);
        Tile FloorTile = (Tile)Floor.GetTile(CellPosition);
        if (FloorTile != null)
        {
            //Create New Node
            GameObject NewNodeObject = Instantiate(NodePrefab,transform);
            NodeScript NewNode = NewNodeObject.GetComponent<NodeScript>();
            NewNode.transform.position = Position;
            NewNode.Connections.Add(Node);
            Node.Connections.Add(NewNode);
            //Generate More Nodes
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(1, 0));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(0, 1));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(-1, 0));
            GenerateNewNodes(NewNode, NewNode.transform.position + new Vector3(0, -1));
        }
    }
}
