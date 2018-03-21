using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(ActionManager))]
[RequireComponent(typeof(KeyManager))]
public class FPSController : MonoBehaviour
{
    [Space(10)]

    public Camera FPSCam;
    public float mouseSensitivity       =  4;
    public float camRotationMaxClamp    =  40;
    public float camRotationMinClamp    = -30;

    [Space(16)]

    public float walkSpeed      = 10f;
    public float runSpeed       = 14f;
    public float jumpSpeed      = 4f;
    public float crouchSpeed    = 2f;
    public float gravity        = 9.81f;

    [Space(16)]

    public float crouchHeight = 0;
    public float popUpMultiplier = 0.38f;
    [Space(6)]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1.5f;
    [Space(6)]
    public float groundDetectionBuffer  = 0.1f;
    public float ceilingDetectionBuffer = 0.2f;
    public float canStandBuffer = 0.25f;

    [Space(16)]

    public bool canStand;
    public bool isCloseToGround;
    public bool running;
    public bool jumping;
    public ActionManager.Action crouching;

    [Space(16)]

    public float YVelocity;

    private CharacterController cc;
    private ActionManager am;
    private KeyManager km;

    private float rotPitch;
    private float characterHeight;


    void Start()
    {
        cc = GetComponent<CharacterController>();
        am = new ActionManager();
        km = GetComponent<KeyManager>();
        characterHeight = cc.height;
    }

    void Awake() { }

    private void Update()
    {
        doActions();
    }

    private void FixedUpdate()
    {
        rotatePlayer();
        movePlayer();
    }

    void doActions()
    {

        canStand        = !Physics.SphereCast(new Ray(transform.position + Vector3.up * characterHeight, Vector3.up),       cc.radius, characterHeight * canStandBuffer);
        isCloseToGround =  Physics.SphereCast(new Ray(transform.position + (cc.radius * 1.1f) * Vector3.up, Vector3.down),  cc.radius, groundDetectionBuffer);


        //If we collide with the ground our velocity should be 0, and we should not be jumping
        if (cc.isGrounded)
        {
            YVelocity = 0;

            if (jumping)
                jumping = false;
        }


        //If we crouch, our height should be as small as possible, else if we want to stand up and have the space to do so, our height should go back to normal
        if (km.getKeyPressing(km.crouchKey))
        {
            am.setDoing(out crouching);
            cc.height = crouchHeight;
        }
        else if (am.doing(crouching) && canStand)
        {
            am.setStopping(out crouching);
            cc.height = characterHeight;
            transform.position += new Vector3(0, characterHeight * popUpMultiplier, 0);
        }
        else if (am.stopping(crouching))
            am.setNotDoing(out crouching);


        if (km.getKeyDown(km.jumpKey) && isCloseToGround && am.notDoing(crouching))
        {
            jumping = true;
            YVelocity = jumpSpeed;
        }


        if (km.getKeyPressing(km.runKey) && am.notDoing(crouching))
            running = true;
        else
            running = false;
    }


    //Sets the rotation of the player based on mouse input
    void rotatePlayer()
    {
        float rotYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotPitch = Mathf.Clamp(rotPitch, camRotationMinClamp, camRotationMaxClamp);

        transform.Rotate(0, rotYaw, 0);
        FPSCam.transform.localRotation = Quaternion.Euler(rotPitch, 0, 0);
    }

    void movePlayer()
    {
        cc.Move((getInputXZ() + getInputY()) * Time.deltaTime);
    }

    Vector3 getInputXZ()
    {
        return transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * getMoveSpeed();
    }

    Vector3 getInputY()
    {
        YVelocity -= gravity * Time.deltaTime;

        //Allows "Mario Jumping": Fall faster, jump higher the longer you press the jump key.
        if (YVelocity < 0)
            YVelocity -= gravity * (fallMultiplier - 1) * Time.deltaTime;
        else if (YVelocity > 0 && !km.getKeyPressing(km.jumpKey))
            YVelocity -= gravity * (lowJumpMultiplier - 1) * Time.deltaTime;

        return new Vector3(0, YVelocity, 0);
    }

    //Return movement speed based on whether the player is walking, running, etc
    float getMoveSpeed()
    {
        if (running)
            return runSpeed;
        if (am.doing(crouching))
            return crouchSpeed;

        return walkSpeed;
    }
}