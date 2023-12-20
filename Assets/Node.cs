using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Tooltip("first is the forward node, second is backward") ]
    public List<Node> connectedNodes = new List<Node>(2);
}
