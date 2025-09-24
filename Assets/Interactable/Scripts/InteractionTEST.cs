

namespace EJETAGame.Inventory
{
    using System.Collections;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using UnityEngine;

    /**
     * Interactable test case where we change the color of a sphere gameobject
     * into a random color;
     */
    public class InteractionTEST : MonoBehaviour, IInteractable
    {
        public Item itemData;

        //Which button the user must press to initiate the Interaction;
        public KeyCode interactionKey;
        public void Interact()
        {
            InventoryManager playerInventory = FindAnyObjectByType<InventoryManager>();

            if (Input.GetKeyDown(interactionKey))
            {
                if (playerInventory != null && itemData != null)
                {
                    playerInventory.AddItem(itemData);
                    Debug.Log("Picked up " + itemData.getItemName());
                    Object.Destroy(gameObject); // currently not working
                }
            
            }

            
        }

        //When our interaction begin, we set the UI text to prompt the user to
        //press a button to interact with the gameobject;
        public void OnInteractEnter()
        {
            InteractionText.instance.SetText("Press "+interactionKey+" to interact");
        }


        //We can debug a statement to let us know when the interaction ends;
        public void OnInteractExit()
        {
            Debug.Log("Interaction Ended");
        }
    }

}
