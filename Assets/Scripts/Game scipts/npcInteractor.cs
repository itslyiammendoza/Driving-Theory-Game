using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteractor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float radius = 3.0f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    public npcController NpcController;

    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, radius, colliders, interactableMask);

        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, radius, colliders, interactableMask);
        //shows the amount of objects interacting with the field

        if (numFound > 0)
        {
            var interactable = colliders[0].GetComponent<npcIInteractable>(); //find monobehaviour implementing the interface
            interactable.Interact(this);
        }
        else
        {
            NpcController.touching = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
        //visualises the field
    }
}
