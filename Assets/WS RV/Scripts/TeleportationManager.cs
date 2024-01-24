using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{

    [SerializeField]
    private TeleportationProvider provider;

    [SerializeField]
    private InputActionReference activate;

    [SerializeField]
    private InputActionReference cancel;

    [SerializeField]
    private XRRayInteractor teleportRay;

    private bool isActive;

    XRInteractorLineVisual lineVisual;


    // Start is called before the first frame update
    void Start()
    {
        teleportRay.enabled = false;

        activate.action.Enable();
        activate.action.performed += OnTeleportActivate; // thumbstick pressed
        activate.action.canceled += OnTeleportRequested; // = thumbstick released

        cancel.action.Enable();
        cancel.action.performed += OnTeleportCancel;

        try
        {
            lineVisual = teleportRay.GetComponent<XRInteractorLineVisual>();
        }
        catch (Exception e)
        {
            Debug.LogError("Error : " + e.Message);
        }
    }


    private void OnTeleportRequested(InputAction.CallbackContext context)
    {
        // check that the teleport isActive to avoid this action: thumbstick pressed -> cancel button pressed -> thumbstick released
        if (!isActive)
            return;
        // thumbstick released so deactivate teleportation and teleport if possible
        RaycastHit hit;
        /* TO DO !!!
         * 
         * Ajouter condition si le Raycast NE touche PAS un �l�ment permettant la t�l�portation alors on annule la t�l�portation
         * 
         * utiser la m�thode TryGetCurrent3DRaycastHit(out RaycastHit hit) du composant XRRayInteractor
         * 
         */

        /*if (false)
        {
            return;
        }*/

        if (!teleportRay.TryGetCurrent3DRaycastHit(out hit))
        {
            teleportRay.enabled = false;
            isActive = false;
            return;
        }

        /* TO DO !!!
             * 
             * Ajouter une methode permettant de faire une teleportation 
             * 
             * 1 . créer une variable TeleportRequest 
             * 
             *      Utiliser la struct TeleportRequest
             * 
             * 2 . Donner le TeleportRequest au TeleportProvider
             * 
             *      Utiliser la m�thode QueueTeleportRequest(TeleportRequest tpRequest) du composant TeleportationProvider
             *      
             */
        /*
         * VERSION 1 TELEPORTATION 
        TeleportRequest tpRequest = new TeleportRequest()
        {
            destinationPosition = hit.point,
        };
        provider.QueueTeleportRequest(tpRequest);
        //teleportRay.enabled = false;
        setActiveTeleport(false);*/

        var interactable = hit.collider.GetComponentInParent<BaseTeleportationInteractable>();
        var t = interactable.GetAttachTransform(teleportRay);

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,

        };

        if (interactable is TeleportationAnchor)
        {
            request = new TeleportRequest()
            {
                destinationPosition = t.position,
                destinationRotation = t.rotation,
                matchOrientation = interactable.matchOrientation
            };
        }
        provider.QueueTeleportRequest(request);
        setActiveTeleport(false);

    }

    private void setActiveTeleport(bool active)
    {
        teleportRay.enabled = lineVisual.enabled = isActive = active;
    }

    private void OnTeleportActivate(InputAction.CallbackContext ctx)
    {
        setActiveTeleport(true);
    }

    private void OnTeleportCancel(InputAction.CallbackContext ctx)
    {
        setActiveTeleport(false);
    }
}
