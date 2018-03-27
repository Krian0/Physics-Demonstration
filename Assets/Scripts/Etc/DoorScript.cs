using UnityEngine;

[RequireComponent(typeof(ActionManager))]
public class DoorScript : MonoBehaviour
{
    public enum DoorState { OPENING, OPEN, CLOSING, CLOSED, ENUM_END };


    public GameObject validUser;
    public float inTimer = 0;
    public float outTimer = 0;
    public float inTimerLimit = 2;
    public float outTimerLimit = 2;

    //// Maximum turn rate in degrees per second.
    //public float turningRate = 30f;
    public float moveSpeed = 5;

    public float detectionBuffer = 0.01f;

    ////public Transform target;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public Transform targetTransform;

    public bool inTriggerZone = false;
    public DoorState state = DoorState.CLOSED;


    void Start ()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (inTriggerZone && state == DoorState.CLOSED)
        {
            inTimer += Time.deltaTime;

            if (inTimer > inTimerLimit)
            {
                state = DoorState.OPENING;
                inTimer = 0;
            }
        }
        else if (!inTriggerZone && state == DoorState.OPEN)
        {
            outTimer += Time.deltaTime;

            if (outTimer > outTimerLimit)
            {
                state = DoorState.CLOSING;
                outTimer = 0;
            }
        }


        if (state == DoorState.OPENING)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetTransform.position) < detectionBuffer)
                state = DoorState.OPEN;

            //if (!inRotation)
            //{
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetTransform.rotation, turningRate * Time.deltaTime);

            //    if (transform.rotation == targetTransform.rotation)
            //        inRotation = true;
            //}
        }

        if (state == DoorState.CLOSING)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, originalPosition) < detectionBuffer)
                state = DoorState.CLOSED;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == validUser)
        {
            inTriggerZone = true;
            outTimer = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == validUser)
        {
            inTriggerZone = false;
            inTimer = 0;
        }
    }
}
