using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera FPSCam;
    public float mouseSensitivity = 4;
    public float camRotationMaxClamp = 40;
    public float camRotationMinClamp = -30;

    public float walkSpeed = 10f;
    public float runSpeed = 14f;
    public float jumpSpeed = 4f;
    public float crouchSpeed = 2f;
    public float gravity = 9.81f;

    public float groundDetectionBuffer = 0.1f;
    public float ceilingDetectionBuffer = 0.2f;

    public bool isGrounded;
    public bool canStand;
    public bool doJump;
    public bool doRun;
    public bool doCrouch;

    private float rotPitch;
    private Vector3 movement;
    private CharacterController cc;

	void Start ()
    {

	}

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        detectActions();
        resolveActions();
        rotatePlayer();
        movePlayer();
    }

    void detectActions()
    {
        isGrounded = Physics.Raycast(transform.position + 0.1f * Vector3.up, Vector3.down, groundDetectionBuffer);
        //canStand = !Physics.Raycast(transform.position + new Vector3(0, cc.height, 0) - 0.1f * Vector3.up, Vector3.down, ceilingDetectionBuffer);
        doJump = Input.GetKey(KeyCode.Space);
        doRun = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        doCrouch = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
    }

    void resolveActions()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            cc.height /= 8;
        if (Input.GetKeyUp(KeyCode.LeftControl))
            cc.height *= 8;
    }

    void rotatePlayer()
    {
        float rotYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        FPSCam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(rotPitch, camRotationMinClamp, camRotationMaxClamp), 0, 0);
        transform.Rotate(0, rotYaw, 0);
    }

    void movePlayer()
    {
        float previousY = movement.y;
        movement = transform.TransformDirection(getMovement()) * getMoveSpeed();

        if (!isGrounded)
            movement.y = previousY;

        movement.y += getYInput() - gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);
    }

    Vector3 getMovement()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    float getYInput()
    {
        return (doJump && isGrounded) ? jumpSpeed : 0;
    }

    float getMoveSpeed()
    {
        if (doRun && isGrounded)
            return runSpeed;
        if (doCrouch && isGrounded)
            return crouchSpeed;

        return walkSpeed;
    }
}