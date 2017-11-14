
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;
    public Transform target;

    public float smoothSpeed = 0.125f ;
    public Vector3 offset;

    void Start()
    {

        target = GameObject.Find("Player").transform;


    }


    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = new Vector3(Mathf.Clamp(smoothedPostion.x, xMin, xMax), Mathf.Clamp(smoothedPostion.y, yMin, yMax), -10);
    }




}