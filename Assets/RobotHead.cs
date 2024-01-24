using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class RobotHead : MonoBehaviour
{

    public XRInteractionManager interactionManager;
    public XRSocketInteractor socketInteractor;
    private XRBaseInteractable currentInteractable;

    // GameObject pour la tête du robot
    public GameObject robotHeadObject;

    // variable booléenne pour vérifier si l'utilisateur a la tête du robot
    private bool hasRobotHead = false;

    public InputActionReference fireAction;
    public InputActionReference grabAction; // Ajout de l'action de saisie

    public InputActionReference grabRemoteAction; // Ajout de l'action de saisie
    public Transform robotHead;
    public Camera mainCamera; // Utilisation de la caméra principale
    public GameObject robot;
    public GameObject extinguisher1; // GameObject pour le premier extincteur
    public GameObject extinguisher2; // GameObject pour le deuxième extincteur
    public GameObject boxIndicator1; // GameObject pour le rectangle du premier extincteur
    public GameObject boxIndicator2; // GameObject pour le rectangle du deuxième extincteur
    private NavMeshAgent robotAgent;
    public float forceMagnitude = 10f;
    public GameObject vfxElement;
    private Vector3 offset1; // Ajout d'un décalage pour l'extincteur
    private Vector3 offset2; // Ajout d'un décalage pour la sphère rouge
    private GameObject grabbedExtinguisher; // Ajout d'un GameObject pour l'extincteur saisi

    // Ajout d'un GameObject pour l'objet à afficher
    public GameObject objectToShow;

    // Ajout d'une variable booléenne pour vérifier si l'utilisateur a l'extincteur
    private bool hasExtinguisher = false;

    // variable booléenne pour vérifier si l'utilisateur a l'objet
    private bool hasObject = false;

    // GameObject pour la sphère rouge
    public GameObject redSphere;

    private GameObject grabbedObject; // Ajout d'un GameObject pour l'objet saisi

    public Transform robotNeck;  // La référence au cou du robot

    private void Start()
    {
        robotAgent = robot.GetComponent<NavMeshAgent>();
        offset1 = new Vector3(0, -0.7f, 0.5f); // Définir le décalage pour représenter la position de la main gauche
        offset2 = new Vector3(-1.0f, 0, 0); // Définir le décalage pour représenter la position de la main droite
    }

    private void Update()
    {

        if (fireAction.action.triggered)
        {
            DropHead();
            ShowBoxIndicators(); // Affiche les rectangles
        }

        // Si l'instance VFX existe, mettez à jour sa position pour qu'elle corresponde à celle de la tête du robot
        if (vfxElement != null)
        {
            vfxElement.transform.position = robotHead.position;
        }

        if (grabAction.action.triggered) // Modification de l'action déclenchée
        {
            MoveGrabbedExtinguisher(); // Déplace l'extincteur saisi avec la caméra
        }

        if (grabRemoteAction.action.triggered) // Modification de l'action déclenchée
        {
            MoveGrabbedObject(); // Déplace l'objet saisi avec la caméra
        }

        if (grabbedExtinguisher != null && grabbedExtinguisher.tag == "extinguisher") // Si un extincteur est saisi
        {
            grabbedExtinguisher.transform.position = mainCamera.transform.position + offset1; // Déplace l'extincteur saisi à la position de la caméra principale avec un décalage
            grabbedExtinguisher.transform.rotation = mainCamera.transform.rotation; // Fait tourner l'extincteur saisi avec la rotation de la caméra principale
        }


        if (grabbedObject != null && grabbedObject.tag == "sphere") // Si un objet est saisi
        {
            grabbedObject.transform.position = mainCamera.transform.position + offset2; // Déplace l'objet saisi à la position de la caméra principale avec un décalage
            grabbedObject.transform.rotation = mainCamera.transform.rotation; // Fait tourner l'objet saisi avec la rotation de la caméra principale
        }

        // Vérifie si l'utilisateur est devant la tête du robot et a l'extincteur
        if (IsInFrontOfRobotHead() && (hasExtinguisher || hasObject))
        {
            // Affiche l'objet au-dessus de la tête du robot
            ShowObjectAboveRobotHead();

            boxIndicator1.SetActive(false);
            boxIndicator2.SetActive(false);

            // Désactive l'élément VFX
            vfxElement.SetActive(false);

            // Désactive les triggers
            fireAction.action.Disable();
            grabAction.action.Disable();

            // Si un extincteur est saisi
            if (grabbedExtinguisher != null && grabbedExtinguisher.tag == "extinguisher")
            {
                // Définir la position de l'extincteur à une position au niveau du sol
                grabbedExtinguisher.transform.position = new Vector3(grabbedExtinguisher.transform.position.x, 0, grabbedExtinguisher.transform.position.z);

                // Réinitialiser l'extincteur saisi
                grabbedExtinguisher = null;
                hasExtinguisher = false;
            }

            // Si un objet est saisi
            if (grabbedObject != null && grabbedObject.tag == "sphere")
            {
                // Définir la position de l'objet à une position au niveau du sol
                grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, 0, grabbedObject.transform.position.z);

                // Réinitialiser l'objet saisi
                grabbedObject = null;
                hasObject = false;
            }

            GrabRobotHead(); // Saisit la tête du robot

            // Vérifie si l'utilisateur est devant le corps du robot et a la tête du robot
            if (IsInFrontOfRobotBody() && hasRobotHead)
            {
                // Remet la tête du robot sur le corps du robot
                AttachRobotHead();

                // Si la tête du robot est saisie
                if (grabbedObject != null && grabbedObject == robotHeadObject)
                {
                    // Réinitialiser l'objet saisi
                    grabbedObject = null;
                    hasRobotHead = false;
                }
            }
        }
    }

    private bool IsInFrontOfRobotBody()
    {
        // Obtenir la position de la caméra
        Vector3 cameraPosition = mainCamera.transform.position;

        // Calculer la direction de la caméra par rapport au robot
        Vector3 directionToCamera = cameraPosition - robot.transform.position;

        // Vérifier si la caméra est devant le robot
        bool isInFront = Vector3.Dot(robot.transform.forward, directionToCamera.normalized) > 0;

        return isInFront;
    }

    private void GrabRobotHead() // Nouvelle fonction pour saisir la tête du robot
    {
        // Code pour saisir la tête du robot
        if (robotHeadObject != null && robotHeadObject.tag == "head") // Si la tête du robot est saisie
        {
            grabbedObject = robotHeadObject; // Définir l'objet saisi comme la tête du robot

            // L'utilisateur a maintenant la tête du robot
            hasRobotHead = true;

            // Utilisez XR Interaction Manager pour saisir la tête du robot
            currentInteractable = robotHeadObject.GetComponent<XRBaseInteractable>();
            interactionManager.SelectEnter(socketInteractor, (IXRSelectInteractable)currentInteractable);
        }
    }

    private void AttachRobotHead() // Nouvelle fonction pour attacher la tête du robot au corps du robot
    {
        // Code pour attacher la tête du robot au cou du robot
        robotHeadObject.transform.position = robotNeck.position; // Déplace la tête du robot à la position du cou du robot
        robotHeadObject.transform.rotation = robotNeck.rotation; // Fait tourner la tête du robot avec la rotation du cou du robot

        // Utilisez XR Interaction Manager pour lâcher la tête du robot
        interactionManager.SelectExit(socketInteractor, (IXRSelectInteractable)currentInteractable);
    }
    private void MoveGrabbedObject() // Nouvelle fonction pour déplacer l'objet saisi avec la caméra
    {
        // Code pour déplacer l'objet saisi avec la caméra
        if (redSphere != null) // Si la sphère rouge est saisi
        {
            grabbedObject = redSphere; // Définir l'objet saisi comme la sphère rouge

            // L'utilisateur a maintenant l'objet
            hasObject = true;
        }
    }

    // Fonction pour vérifier si l'utilisateur est devant la tête du robot
    private bool IsInFrontOfRobotHead()
    {
        // Définir une distance minimale pour être "devant" la tête du robot
        float minDistance = 2.0f; // Ajustez cette valeur comme vous le souhaitez

        // Calculer la distance entre la caméra et la tête du robot
        float distance = Vector3.Distance(mainCamera.transform.position, robotHead.position);

        // Si la caméra est trop loin, retourner false
        if (distance > minDistance)
        {
            return false;
        }

        // Calculer la direction de la caméra à la tête du robot
        Vector3 directionToRobotHead = (robotHead.position - mainCamera.transform.position).normalized;

        // Calculer le produit scalaire entre la direction de la caméra et la direction vers la tête du robot
        float dotProduct = Vector3.Dot(mainCamera.transform.forward, directionToRobotHead);

        // Si le produit scalaire est positif, la caméra est orientée vers la tête du robot
        if (dotProduct > 0)
        {
            return true;
        }

        // Sinon, la caméra n'est pas orientée vers la tête du robot
        return false;
    }

    // Fonction pour afficher l'objet au-dessus de la tête du robot
    private void ShowObjectAboveRobotHead()
    {
        // Assurez-vous que l'objet à afficher est actif
        objectToShow.SetActive(true);

        // Positionne l'objet au-dessus de la tête du robot
        objectToShow.transform.position = robotHead.position + new Vector3(0, 0, 0); // Ajustez le décalage comme vous le souhaitez
    }

    private void DropHead()
    {
        robotHead.parent = null;
        robotAgent.enabled = false;

        // Récupérer le Rigidbody existant
        Rigidbody rigidBody = robotHead.GetComponent<Rigidbody>();

        Vector3 forceDirection = new Vector3(1, 0, 0);
        rigidBody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

        // Commencez une coroutine pour activer l'élément VFX après un délai
        StartCoroutine(ActivateVFXAfterDelay(2f));
    }


    private IEnumerator ActivateVFXAfterDelay(float delay)
    {
        // Attendez le délai spécifié
        yield return new WaitForSeconds(delay);

        // Activez l'instance de l'élément VFX
        vfxElement.SetActive(true);
    }

    private void ShowBoxIndicators() // Affiche les rectangles
    {
        boxIndicator1.SetActive(true);
        boxIndicator2.SetActive(true);
    }

    private void MoveGrabbedExtinguisher() // Nouvelle fonction pour déplacer l'extincteur saisi avec la caméra
    {
        // Code pour déplacer l'extincteur saisi avec la caméra
        if (extinguisher1 != null) // Si le premier extincteur est saisi
        {
            grabbedExtinguisher = extinguisher1; // Définir l'extincteur saisi comme le premier extincteur

            // L'utilisateur a maintenant l'extincteur
            hasExtinguisher = true;
        }
    }
}
