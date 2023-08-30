using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, radius, colliders, interactableMask);
        //shows the amount of objects interacting with the field

        if (numFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>(); //find monobehaviour implementing the interface

            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
                //if there is something interacting with the sphere and the user presses E then the funcitonality of that object activates
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
        //visualises the field
    }
}
