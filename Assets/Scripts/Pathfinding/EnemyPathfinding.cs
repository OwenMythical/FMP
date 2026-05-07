using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public NodeScript CurrentNode;
    public List<NodeScript> Path = new List<NodeScript>();
    public PolygonCollider2D VisionCone;
    public EnemyHealth EH;
    public ContactDamage HB;
    GameObject Player;
    Rigidbody2D RB;
    NodeScript NextNode;
    Bounds PlayerBound = new Bounds();
    Bounds EnemyBound = new Bounds();
    Bounds RandomBound = new Bounds();
    int Boredom = 99999;
    public int Interest = 0;
    int WanderCooldown = 0;
    public int MaxBoredom = 1000;
    public int MaxInterest = 150;
    public int MaxWanderCooldown = 1000;
    int i = 0;
    int EM = 0;
    bool PlayerSpotted = false;
    bool Wandering = false;
    bool Distracted = false;
    Vector2 OriginPosition;
    public float Speed = 1;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = (Rigidbody2D)gameObject.GetComponent("Rigidbody2D");
        PlayerBound.size = new Vector2(1, 1);
        EnemyBound.size = new Vector2(1, 1);
        RandomBound.size = new Vector2(1, 1);
        OriginPosition = transform.position;
    }

    private void Update()
    {
        EnemyBound.center = transform.position;
        foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
        {
            if (EnemyBound.Contains(NodeCheck.transform.position))
            {
                CurrentNode = NodeCheck;
                break;
            }
        }
        RB.velocity = new Vector2(0, 0);
        if (NextNode != null)
        {
            Vector3 rotation = NextNode.transform.position - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotZ + 90), 3.5f);
        }
        if (EH.CanMove == true && HB.CanAttack == true)
        {
            CreatePath();
        }
    }

    public void CreatePath()
    {
        //Raycast to check for walls
        Vector3 Direction = Player.transform.position - gameObject.transform.position;
        List<RaycastHit2D> Results = new List<RaycastHit2D>();
        if (Physics2D.Raycast(transform.position, Direction, new ContactFilter2D(), Results) > 0)
        {
            if (Results[1].collider.gameObject == Player.gameObject && VisionCone.OverlapPoint(Player.transform.position))
            {
                PlayerSpotted = true;
                OriginPosition = CurrentNode.transform.position;
                Boredom = 0;
                Interest += 1;
            }
            else
            {
                PlayerSpotted = false;
                Boredom += 1;
            }
        }
        else
        {
            PlayerSpotted = false;
            Boredom += 1;
        }
        if (Boredom > MaxBoredom)
        {
            Boredom = MaxBoredom;
        }
        if (Interest > MaxInterest)
        {
            Interest = MaxInterest;
        }
        if (Boredom >= MaxBoredom)
        {
            Interest = 0;
        }
        if (Path.Count > 0)
        {
            EM += 1;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Path[0].transform.position.x, Path[0].transform.position.y), Speed * Time.deltaTime);
            NextNode = Path[0];

            if (Vector2.Distance(transform.position, Path[0].transform.position) < 0.5f)
            {
                CurrentNode = Path[0];
                Path.RemoveAt(0);
                EM = 0;
                i += 1;
                if (i >= 2 && (PlayerSpotted == true || (Wandering == false && Distracted == false)))
                {
                    Path.Clear();
                    i = 0;
                }
                if (Wandering == true && PlayerSpotted == true)
                {
                    Path.Clear();
                    Wandering = false;
                    i = 0;
                }
                if (Distracted == true && PlayerSpotted == true)
                {
                    Path.Clear();
                    Distracted = false;
                    i = 0;
                }
            }
            if (EM > 1000)
            {
                Debug.Log("Path Took Too Long");
                Path.Clear();
                i = 0;
                EM = 0;
            }
        }
        else
        {
            if (Physics2D.Raycast(transform.position, Direction, new ContactFilter2D(), Results) > 0)
            {
                if ((Results[1].collider.gameObject == Player.gameObject && VisionCone.OverlapPoint(Player.transform.position) && Interest >= MaxInterest) || (Boredom < MaxBoredom && Interest >= MaxInterest))
                {
                    //Chase Player
                    Wandering = false;
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
                    if (CurrentNode != null && ObjectiveNode != null)
                    {
                        Path = AStarManager.Instance.GeneratePath(CurrentNode, ObjectiveNode); //Start, End
                        NodeScript CurrentNodeTEST = CurrentNode;
                        float J = 0;
                        foreach (NodeScript Node in Path)
                        {
                            J += 0.025f;
                            Debug.DrawLine(CurrentNodeTEST.transform.position, Node.transform.position, new Color(0, J, 1), 0.5f);
                            CurrentNodeTEST = Node;
                        }
                        EM = 0;
                        i = 0;
                    }
                }
                else if (Boredom >= MaxBoredom && WanderCooldown >= MaxWanderCooldown && Distracted == false)
                {
                    WanderCooldown = 0;
                    Wandering = true;
                    //Choose Random Point To Wander To
                    float X = OriginPosition.x + Random.Range(-2, 3);
                    float Y = OriginPosition.y + Random.Range(-2, 3);
                    RandomBound.center = new Vector2(X, Y);
                    NodeScript[] Nodes = FindObjectsOfType<NodeScript>();
                    NodeScript ObjectiveNode = CurrentNode;
                    foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
                    {
                        if (RandomBound.Contains(NodeCheck.transform.position))
                        {
                            ObjectiveNode = NodeCheck;
                            break;
                        }
                    }
                    if (CurrentNode != null && ObjectiveNode != null)
                    {
                        Path = AStarManager.Instance.GeneratePath(CurrentNode, ObjectiveNode); //Start, End
                        NodeScript CurrentNodeTEST = CurrentNode;
                        float J = 0;
                        foreach (NodeScript Node in Path)
                        {
                            J += 0.025f;
                            Debug.DrawLine(CurrentNodeTEST.transform.position, Node.transform.position, new Color(0, 1, J), 0.5f);
                            CurrentNodeTEST = Node;
                        }
                        EM = 0;
                        i = 0;
                    }
                }
                else
                {
                    WanderCooldown += 1;
                }
            }
        }
    }

    public void Distract(Vector2 DistractionPos)
    {
        if (PlayerSpotted == false)
        {
            Distracted = true;
            Wandering = false;
            RandomBound.center = DistractionPos;
            NodeScript[] Nodes = FindObjectsOfType<NodeScript>();
            NodeScript ObjectiveNode = CurrentNode;
            foreach (NodeScript NodeCheck in FindObjectsOfType<NodeScript>())
            {
                if (RandomBound.Contains(NodeCheck.transform.position))
                {
                    ObjectiveNode = NodeCheck;
                    break;
                }
            }
            if (CurrentNode != null && ObjectiveNode != null)
            {
                Path = AStarManager.Instance.GeneratePath(CurrentNode, ObjectiveNode); //Start, End
                NodeScript CurrentNodeTEST = CurrentNode;
                float J = 0;
                foreach (NodeScript Node in Path)
                {
                    J += 0.025f;
                    Debug.DrawLine(CurrentNodeTEST.transform.position, Node.transform.position, new Color(1, 1, J), 0.5f);
                    CurrentNodeTEST = Node;
                }
                EM = 0;
                i = 0;
            }
        }
    }

    public void Damaged()
    {
        //Chase Player
        Boredom = 0;
        Interest = MaxInterest;
        Wandering = false;
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
        if (CurrentNode != null && ObjectiveNode != null)
        {
            Path = AStarManager.Instance.GeneratePath(CurrentNode, ObjectiveNode); //Start, End
            NodeScript CurrentNodeTEST = CurrentNode;
            float J = 0;
            foreach (NodeScript Node in Path)
            {
                J += 0.025f;
                Debug.DrawLine(CurrentNodeTEST.transform.position, Node.transform.position, new Color(0, J, 1), 0.5f);
                CurrentNodeTEST = Node;
            }
            EM = 0;
            i = 0;
        }
    }
}
