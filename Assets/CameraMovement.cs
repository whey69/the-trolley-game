using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float zoom;
    private Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        gameObject.transform.position += speed * Time.deltaTime * movement;

        float zoomDelta = Input.mouseScrollDelta.y;
        float newSize = cam.orthographicSize - zoomDelta;
        newSize = Mathf.Clamp(newSize, 1, 10);

        cam.orthographicSize = newSize;
    }
}