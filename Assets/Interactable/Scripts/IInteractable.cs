
namespace EJETAGame {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public interface IInteractable
    {
        GameObject gameObject { get; }
        //Called when we want to interact with the gameobject, eg. when clicking a button;
        void Interact()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacted with " + gameObject.name);
            }
        }

        //Called when detection with the object starts;
        void OnInteractEnter()
        {
            InteractionText.instance.SetText("Press E to interact with " + gameObject.name);
        }

        //Called when detection with the object ends;
        void OnInteractExit()
        {
            Debug.Log("Interaction ended");
        }  
    }
}

