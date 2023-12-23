using DG.Tweening;
using UnityEditor.Callbacks;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject focus;

    [SerializeField]
    float speed;
    float zoom;
    private Camera cam;
    private GameObject previousFocus;
    private Tweener tweezer; 

    Rigidbody2D rb;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        zoom = cam.orthographicSize;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            focus = null;
        }

        // todo: uncomment all of the stuff below and figure out how to use dotween on a moving destination
    //     if (focus != previousFocus)
    //     {
    //         tweezer = rb.DOMove(focus.transform.position + new Vector3(0, 0, -10), 1.5f).SetSpeedBased(true);
    //     }
    //     if(tweezer.active)
    //     {
    //         tweezer.onUpdate(delegate (){
    //             if(Vector3.Distance(rb.position, focus.transform.position) > completionRadius) {
	//     tweener.ChangeEndValue(followTarget.position, true);
	// }
    //         });
    //     }

        if (focus == null)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rb.position += speed * Time.deltaTime * movement;
        }
        else
        {
            // if (!DOTween.IsTweening(rb))
            // {
                rb.position = focus.transform.position + new Vector3(0, 0, -10);
            // }
        }
        previousFocus = focus;

        float zoomDelta = Input.mouseScrollDelta.y;
        float newSize = cam.orthographicSize - zoomDelta;
        newSize = Mathf.Clamp(newSize, 1, 10);

        cam.orthographicSize = newSize;
    }
}
