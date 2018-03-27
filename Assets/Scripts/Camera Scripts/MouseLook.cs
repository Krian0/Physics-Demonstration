using UnityEngine;


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  MOUSE LOOK
//
//  Contains:   A float for mouse sensitivity;
//              A float for the minimum pitch clamp;
//              A float for the maximum pitch clamp;
//              A GameObject for the Player/Controlled Character;
//              A float for Yaw;
//              A float for Pitch;
//
//  Use:        Add this script to the Camera object you wish to use;
//              Set the Camera as a child of the Player/Controlled Character's GameObject;
//              Drag the Player/Controlled Character's GameObject to the appropriate variable in the Inspector;
//
//  Purpose:    To encapsulate mouse-specific Camera behaviour;
//              To set the up/down rotation of the Camera and the sideways rotation of the Player/Controlled Character based on mouse movement;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      Can be used for over-the-shoulder POV as well as first-person POV;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


[RequireComponent(typeof(Camera))]
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 4;

    public float camRotationMinClamp = -60;
    public float camRotationMaxClamp = 80;

    [Space(16)]

    public GameObject player;

    [Space(16)]

    public float yaw;
    public float pitch;


    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void FixedUpdate()
    {
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, camRotationMinClamp, camRotationMaxClamp);

        if (GetComponentInParent<FPSController>().canMove)
        {
            player.transform.Rotate(0, yaw, 0);
            transform.localRotation = Quaternion.Euler(pitch, player.transform.rotation.y, player.transform.rotation.z);
        }
    }
}