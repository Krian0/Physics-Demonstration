using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Ragdoll ragdoll = other.GetComponent<Ragdoll>();
        if (ragdoll != null)
            ragdoll.RagdollOn = true;
    }
}
