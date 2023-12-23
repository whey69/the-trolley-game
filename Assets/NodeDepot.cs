using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Route
{
    public int routeNumber;
    public List<NodeStation> stations;
    public bool isLoop;

    [HideInInspector]
    public List<TrolleyBus> trolleyBusesOnLine;
    public int firstStationIndex;
}

public class NodeDepot : Node
{
    public int trolleyBusesInStore;
    public int depotNumber;

    [SerializeField]
    public List<Route> routes;

    [SerializeField]
    GameObject trolleyprefab;

    private int trolleysSent;

    void SendTrolleybus()
    {
        GameObject trolley = Instantiate(
            trolleyprefab,
            gameObject.transform.position,
            quaternion.identity
        );
        trolley.transform.SetParent(GameObject.FindWithTag("TrolleyManager").transform);
        Debug.Log(trolleysSent % routes.Count);
        trolley
            .GetComponent<TrolleyBus>()
            .startTrolley(
                routes[trolleysSent % routes.Count],
                (routes[trolleysSent % routes.Count].routeNumber * 100) + UnityEngine.Random.Range(1, 99)
            );
        trolleyBusesInStore--;
        trolleysSent++;
    }

    void Update()
    {
        // once again for testing
        if (Input.GetKeyDown("e"))
        {
            SendTrolleybus();
        }
    }
}
