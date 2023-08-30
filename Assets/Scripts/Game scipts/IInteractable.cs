using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{//represents contract between the user and object
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor);
    //returns a bool if an interaction occured
}
