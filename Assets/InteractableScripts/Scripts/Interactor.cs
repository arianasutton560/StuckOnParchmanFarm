
namespace EJETAGame
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;

    public class Interactor : MonoBehaviour
    {
        [SerializeField] Transform interactorSource; //Point of origin for our Interaction source
        [SerializeField] float interactRange; //How far our Interaction can detect;

        private IInteractable currentInteractable; //Track the currently detected interactable object;

        public GameObject detectedObject;


        private void Update()
{
            Ray r = new Ray(interactorSource.position, interactorSource.forward);

            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                detectedObject = hitInfo.collider.gameObject;

                IInteractable interactObj = null;

                if (!detectedObject.TryGetComponent(out interactObj))
                {
                    interactObj = detectedObject.GetComponentInParent<IInteractable>();
                }

                if (interactObj != null)
                {
                    if (currentInteractable != interactObj)
                    {
                        if (currentInteractable != null)
                            currentInteractable.OnInteractExit();

                        interactObj.OnInteractEnter();
                        currentInteractable = interactObj;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactObj.Interact();
                    }

                    InteractionText.instance.textAppear.gameObject.SetActive(true);
                }
                else
                {
                    if (currentInteractable != null)
                    {
                        InteractionText.instance.textAppear.gameObject.SetActive(false);
                        currentInteractable.OnInteractExit();
                        currentInteractable = null;
                    }
                }
            }
            else
            {
                
                InteractionText.instance.textAppear.gameObject.SetActive(false);

                if (currentInteractable != null)
                {
                    currentInteractable.OnInteractExit();
                    currentInteractable = null;
                }
            }
        }




    }
}

