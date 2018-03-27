using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FPSController))]
public class AnimationHelper : MonoBehaviour
{
    public float baseSpeed = 80.00f;

    private Animator a;
    private FPSController fpsC;

    void Start ()
    {
        a = GetComponent<Animator>();
        fpsC = GetComponent<FPSController>();
    }

    void Update ()
    {
        a.SetFloat("Speed", Input.GetAxis("Vertical") * baseSpeed * fpsC.getMoveSpeed() * Time.deltaTime);
    }
}
