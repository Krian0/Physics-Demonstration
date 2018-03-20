using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ActionManager))]
[RequireComponent(typeof(KeyManager))]
public class FPSController : MonoBehaviour
{
    [Space(10)]

    public Camera FPSCam;
    public float mouseSensitivity = 4;
    public float camRotationMaxClamp = 40;
    public float camRotationMinClamp = -30;

    [Space(16)]


    public float walkSpeed = 10f;
    public float runSpeed = 14f;
    public float jumpSpeed = 4f;
    public float crouchSpeed = 2f;
    public float gravity = 9.81f;

    [Space(10)]

    public float groundDetectionBuffer = 0.1f;
    public float ceilingDetectionBuffer = 0.2f;

    [Space(16)]

    public bool canStand;
    public bool isGrounded;
    public bool running;
    public bool jumping;
    public ActionManager.Action crouching;


    private CharacterController cc;
    private ActionManager am;
    private KeyManager km;
    private Vector3 movement;
    private float valueY;
    private float rotPitch;
    private float characterHeight;



    void Start()
    {
        cc = GetComponent<CharacterController>();
        km = GetComponent<KeyManager>();
        characterHeight = cc.height;
    }

    void Awake() { }

    void Update()
    {
        detectActions();
        doActions();
        rotatePlayer();
        movePlayer();
    }

    void detectActions()
    {
        canStand = !Physics.SphereCast(new Ray(transform.position + Vector3.up * characterHeight, Vector3.up), cc.radius, characterHeight / 2.9f);
        isGrounded = Physics.SphereCast(new Ray(transform.position + (cc.radius * 1.1f) * Vector3.up, Vector3.down), cc.radius, groundDetectionBuffer);


        if (km.getKeyPressing(km.crouchKey))
            am.setDoing(out crouching);
        else if (am.doing(crouching) && canStand)
            am.setStopping(out crouching);
        else
            am.setNotDoing(out crouching);


        if (km.getKeyDown(km.jumpKey) && isGrounded)
            jumping = true;
        else if (jumping && movement.y < -1)
            jumping = false;


        if (km.getKeyPressing(km.runKey) && am.notDoing(crouching))
            running = true;
        else
            running = false;
    }

    void doActions()
    {
        if (am.doing(crouching))
            cc.height = 0;
        else if (am.stopping(crouching))
        {
            valueY = 2;
            cc.height = characterHeight;
        }
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
        movement = getInputXZ() + getInputY();
        cc.Move(movement * Time.deltaTime);
        valueY = 0;
    }

    Vector3 getInputXZ()
    {
        return transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * getMoveSpeed();
    }

    Vector3 getInputY()
    {
        valueY -= gravity * Time.deltaTime;

        //Must use cc.isGrounded due to it being based on bottom capsule collision, so that the player falls accurately until landing on something
        if (!cc.isGrounded)
            valueY += movement.y;
        if (jumping && cc.isGrounded)
            valueY += jumpSpeed;

        return new Vector3(0, valueY, 0);
    }

    float getMoveSpeed()
    {
        if (running)
            return runSpeed;
        if (am.doing(crouching))
            return crouchSpeed;

        return walkSpeed;
    }
}