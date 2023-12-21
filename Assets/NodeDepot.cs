using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Route
{
    public List<NodeStation> stations;
    [HideInInspector] public List<TrolleyBus> trolleyBusesOnLine;
    public int firstStationIndex;
}

public class NodeDepot : Node
{
    public int trolleyBusesInStore;
    public int depotNumber;
    [SerializeField] public List<Route> routes;
    [SerializeField] GameObject trolleyprefab;

    void SendTrolleybus()
    {
        // TrolleyBus trolley = new TrolleyBus(routes[0], routes[0].firstStationIndex); // "routes[0]" temp, add another system later pls ok thx bye
        GameObject trolley = Instantiate(trolleyprefab, gameObject.transform.position, quaternion.identity);
        trolley.transform.SetParent(GameObject.FindWithTag("TrolleyManager").transform);
        trolley.GetComponent<TrolleyBus>().startTrolley(routes[0], routes[0].firstStationIndex);
        trolley.GetComponent<TrolleyBus>().id = UnityEngine.Random.Range(1, 100);
        trolleyBusesInStore -= 1;
    }

    void Update()
    {
        // once again for testing
        if(Input.GetKeyDown("e"))
        {
            SendTrolleybus();
        }
    }
}