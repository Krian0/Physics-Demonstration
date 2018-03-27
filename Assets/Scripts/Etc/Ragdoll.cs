using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    private Animator animator = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();


    public bool RagdollOn
    {
        get { return !animator.enabled; }
        set
        {
            GetComponent<FPSController>().canMove = !value;

            animator.enabled = !value;
            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;
        }
    }

	void Start ()
    {
        animator = GetComponent<Animator>();

        rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
	}
	
	void Update ()
    {
        
	}
}
