using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public NodeScript CurrentNode;
    public List<NodeScript> Path = new List<NodeScript>();
    GameObject Player;
    Rigidbody2D RB;
    SpriteRenderer SR;
    Bounds PlayerBound = new Bounds();
    Bounds EnemyBound = new Bounds();
    int i = 0;
    public float Speed = 1;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = (Rigidbody2D)gameObject.GetComponent("Rigidbody2D");
        SR = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
        SR.enabled = false;
        PlayerBound.size = new Vector2(1, 1);
        EnemyBound.size = new Vector2(1, 1);
    }

    private void Update()
    {
        RB.velocity = new Vector2(0, 0);
        Vector3 rotation = Player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotZ + 90), 1.5f);
        CreatePath();
    }

    public void CreatePath()
    {
        if (Path.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Path[0].transform.position.x, Path[0].transform.position.y), Speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, Path[0].transform.position) < 0.5f)
            {
                CurrentNode = Path[0];
                Path.RemoveAt(0);
                i += 1;
                if (i == 2)
                {
                    Path.Clear();
                    i = 0;
                }
            }
        }
        else
        {
            NodeScript[] Nodes = FindObjectsOfType<NodeScript>();
            PlayerBound.center = Player.transform.position;
            NodeScript ObjectiveNode = CurrentNode;
            foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
            {
                if (PlayerBound.Contains(NodeCheck.transform.position))
                {
                    ObjectiveNode = NodeCheck;
                    break;
                }
                
            }
            if (CurrentNode != null)
            {
                while (Path == null || Path.Count == 0)
                {
                    Path = AStarManager.Instance.GeneratePath(CurrentNode, ObjectiveNode); //Start, End
                }
            }
            else
            {
                foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
                {
                    if (EnemyBound.Contains(NodeCheck.transform.position))
                    {
                        CurrentNode = NodeCheck;
                        break;
                    }

                }
            }
        }
    }
}
