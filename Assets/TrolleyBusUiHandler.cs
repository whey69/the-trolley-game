using TMPro;
using UnityEngine;

public class TrolleyBusUiHandler :  MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text id;
    [SerializeField] private TMP_Text passes;
    [SerializeField] private TMP_Text nextstop;
    private TrolleyBus trolley;

    void Start()
    {
        trolley = gameObject.GetComponent<TrolleyBus>();
    }

    void Update()
    {
        id.text = $"Trolleybus #{trolley.id}";
        passes.text = trolley.passengers.ToString();
        nextstop.text = trolley.nextStation.stationName;
    }

    void OnMouseOver()
    {
        ui.SetActive(true);
    }

    void OnMouseExit()
    {
        ui.SetActive(false);
    }
}