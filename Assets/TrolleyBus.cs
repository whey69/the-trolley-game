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
    public int id;
    public NodeStation nextStation;

    private bool isActive = false;
    private int nodeIndex;
    private bool stopped;
    private float pickedUpTimer; // maximum should be 5, pickedUpTimer += 0.1 * dt

    void MoveTrolley()
    {
        // https://imgur.com/a/9gVhXlo
        // Vector3 targetPosition = nextStation.transform.position;
        // t += Time.deltaTime * maxSpeed / Vector3.Distance(transform.position, targetPosition);
        // float smoothT = Mathf.SmoothStep(0f, 1f, t);
        // currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, smoothT * acceleration * Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position, targetPosition, smoothT);

        Vector3 pos = nextStation.transform.position;

        if (!stopped)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                pos,
                maxSpeed * Time.deltaTime
            );

            Vector3 direction = (pos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            gameObject.GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                Time.deltaTime * 999
            );
        }

        if (Vector3.Distance(gameObject.transform.position, pos) <= .01f && !stopped)
        {
            stopped = true;

            passengers += nextStation.passengers;
            passengers -= UnityEngine.Random.Range(0, passengers/2);

            pickedUpTimer = 5f;
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

        if (pickedUpTimer <= 0f)
        {
            stopped = false;
        }
        else
        {
            pickedUpTimer -= Time.deltaTime;
        }
        // Debug.Log($"{pickedUpTimer}; {stopped};");
    }

    void Update()
    {
        if (isActive)
        {
            MoveTrolley();
        }
    }

    public void startTrolley(Route route, int firstStationIndex, int id)
    {
        routeStations = route;
        nextStation = routeStations.stations[firstStationIndex];
        nodeIndex = firstStationIndex;
        isActive = true;
        this.id = id;
    }
}
