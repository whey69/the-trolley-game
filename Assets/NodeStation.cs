using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

// if i have to hack this in then i wonder how many hacks there are in the unity source code 
[Serializable]
public class nextRouteStop
{
    public int routeNumber;
    public Node nextNode;
}

public class NodeStation : Node
{
    public string stationName;
    public int passengers;
    public int maxPassengers;
    public List<nextRouteStop> nextStation;
    private int previousPassengers;

    void Start()
    {
        gameObject.GetComponentInChildren<TMP_Text>().text = stationName;
    }

    void Update()
    {
        if (passengers - previousPassengers != 0)
        {
            passengers = UnityEngine.Random.Range(0, maxPassengers);
        }
    }
}