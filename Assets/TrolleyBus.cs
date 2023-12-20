using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TrolleyBus : MonoBehaviour
{
    public int route;
    public Route routeStations;
    public int passengers;
    public float maxSpeed;
    public float acceleration;

    private bool isActive = false;
    private NodeStation nextStation;
    private int nodeIndex;
    private float currentSpeed;

    void MoveTrolley()
    {
        // https://imgur.com/a/9gVhXlo
        // Vector3 targetPosition = nextStation.transform.position;
        // t += Time.deltaTime * maxSpeed / Vector3.Distance(transform.position, targetPosition);
        // float smoothT = Mathf.SmoothStep(0f, 1f, t);
        // currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, smoothT * acceleration * Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position, targetPosition, smoothT);

        Vector3 pos = nextStation.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(
            gameObject.transform.position,
            pos,
            maxSpeed * Time.deltaTime
        );

        Vector3 direction = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward),
            Time.deltaTime * 999
        );

        if ((pos - gameObject.transform.position).magnitude <= .01f)
        {
            // todo: add logic to get passengers from the station and put them on the trolley

            if (nodeIndex >= routeStations.stations.Count - 1)
            {
                nodeIndex = 0;
            }
            else
            {
                nodeIndex++;
            }
            nextStation = routeStations.stations[nodeIndex];
        }
    }

    void Update()
    {
        if (isActive)
        {
            MoveTrolley();
        }
    }

    public void startTrolley(Route route, int firstStationIndex)
    {
        routeStations = route;
        nextStation = routeStations.stations[firstStationIndex];
        nodeIndex = firstStationIndex;
        isActive = true;
    }
}