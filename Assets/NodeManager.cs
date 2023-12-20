using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    GameObject connecterPrefab;
    Node[] nodes;
    List<GameObject> nodeConnectors = new List<GameObject>();

    void Start()
    {
        recalculateConnectors();
    }

    // hopefully this wont become an optimisation issue later
    public void recalculateConnectors()
    {
        foreach(GameObject connecter in nodeConnectors)
        {
            Destroy(connecter);
        }

        nodes = gameObject.GetComponentsInChildren<Node>();
        Node[] connectedNodes = new Node[nodes.Length];
        foreach (Node node in nodes)
        {
            foreach (Node conNode in node.connectedNodes)
            {
                if (connectedNodes.Contains<Node>(conNode))
                    continue;
                Vector3 centerPoint = (node.transform.position + conNode.transform.position) / 2;
                float magnitude =
                    (node.transform.position - conNode.transform.position).magnitude;
                GameObject connecter = Instantiate(connecterPrefab);
                connecter.transform.position = centerPoint;
                connecter.transform.localScale = new Vector3(magnitude, .05f, 1);

                // tjanks chatgbt ⚠️
                Vector3 direction = node.transform.position - centerPoint;
                connecter.transform.up = direction.normalized;
                Quaternion additionalRotation = Quaternion.Euler(0, 0, 90);
                connecter.transform.rotation *= additionalRotation;

                nodeConnectors.Append(connecter);
            }
            connectedNodes.Append(node);
        }
    }
}
