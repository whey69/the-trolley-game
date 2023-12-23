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