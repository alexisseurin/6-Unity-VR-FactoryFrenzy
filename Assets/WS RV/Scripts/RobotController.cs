using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RobotController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference trigger;

    [SerializeField]
    private InputActionReference cancel;

    [SerializeField]
    private XRRayInteractor ray;

    [SerializeField]
    private CallRobotConfig config;

    // Ajoutez une référence à la caméra
    [SerializeField]
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        ray.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ray.enabled)
            return;

        if (trigger.action.IsPressed())
        {
            RaycastHit hit;
            // Ajouter la condition si le ray touche quelquechose
            if (ray.TryGetCurrent3DRaycastHit(out hit))
            {
                // Ajouter la m�thode pour configurer la destination du robot au point ou le rayon touche
                config.CallMe(hit.point);
                ray.enabled = false;
                return;
            }
        }
        else if (cancel.action.IsPressed())
        {
            ray.enabled = false;
        }

    }

    public void Patrol()
    {
        config.ResetCall();
    }

    public void GoTo()
    {
        ray.enabled = true;
    }

    public void Calling(Transform t)
    {
        config.CallMe(t.position + new Vector3(0.25f, 0, 0.25f));
    }

    // Ajoutez une nouvelle méthode pour appeler le robot à la position de la caméra
    public void CallToCameraPosition()
    {
        config.CallMe(mainCamera.transform.position);
    }
}
