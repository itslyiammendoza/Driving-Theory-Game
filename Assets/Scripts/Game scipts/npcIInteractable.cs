using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface npcIInteractable
{
    public string InteractionPrompt { get; }
    public bool Interact(npcInteractor interactor);
    //returns a bool if an interaction occured
}
