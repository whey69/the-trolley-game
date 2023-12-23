using UnityEngine;
using TMPro;

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

    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private TMP_Text idText;

    [SerializeField]
    private TMP_Text passes;

    [SerializeField]
    private TMP_Text nextstop;

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
            idText.text = $"Trolleybus #{id}";
            passes.text = passengers.ToString();
            nextstop.text = nextStation.stationName;

            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                pos,
                maxSpeed * Time.deltaTime
            );

            Vector3 direction = (pos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            gameObject.GetComponentInChildren<SpriteRenderer>().transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.AngleAxis(angle, Vector3.forward),
                    Time.deltaTime * 999
                );
        }

        if (Vector3.Distance(gameObject.transform.position, pos) <= .01f && !stopped)
        {
            stopped = true;

            passengers += nextStation.passengers / 3;
            passengers += passengers + (Random.Range(1, 11) * ((Random.Range(0f, 1f) < 0.6f) ? 1 : -1));
            passengers = Mathf.Max(0, passengers);

            pickedUpTimer = 5f;
            if (nodeIndex >= routeStations.stations.Count - 1)
            {
                if (!routeStations.isLoop)
                {
                    routeStations.stations.Reverse();
                    nodeIndex = 1; // skip 0 because we already reached it
                }
                else
                {
                    nodeIndex = 0;
                }
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

    public void startTrolley(Route route, int id)
    {
        routeStations = route;
        nextStation = routeStations.stations[routeStations.firstStationIndex];
        nodeIndex = routeStations.firstStationIndex;
        isActive = true;
        this.id = id;
    }

    void OnMouseOver()
    {
        ui.SetActive(true);
        
        if(Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().focus = gameObject;
        }
    }

    void OnMouseExit()
    {
        ui.SetActive(false);
    }
}
