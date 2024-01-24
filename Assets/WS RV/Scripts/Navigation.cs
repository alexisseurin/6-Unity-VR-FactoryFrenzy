using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Navigation : MonoBehaviour
{

    public XRInteractionManager interactionManager;
    public XRSocketInteractor socketInteractor;
    private XRBaseInteractable currentInteractable;


    private GameObject grabbedObject; // Ajout d'un GameObject pour l'objet saisi
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    public NavMeshAgent agent;
    public float stopDistance = 1.0f;
    public Transform leftArm;  // La référence au bras gauche du robot
    public Transform rightArm;  // La référence au bras droit du robot
    public Transform leftElbow;  // La référence au coude gauche du robot
    public Transform rightElbow;  // La référence au coude droit du robot
    public Transform leftHand;  // La référence à la main gauche du robot
    public Transform rightHand;  // La référence à la main droite du robot
    private float armLowerSpeed = 1f;  // La vitesse à laquelle les bras s'abaissent
    public Transform robotHead;  // La référence à la tête du robot
    public Camera mainCamera; // Utilisation de la caméra principale

    public GameObject robot;

    // GameObject pour la tête du robot
    public GameObject robotHeadObject;

    public Transform robotNeck;  // La référence au cou du robot

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null && agent.isOnNavMesh)
        {
            agent.destination = patrolPoints[currentPoint].position;
            agent.stoppingDistance = stopDistance;
        }
        // Appeler LowerArms() pour faire bouger les bras du robot
        StartCoroutine(LowerArms());
    }

    void Update()
    {
        if (agent != null && agent.isOnNavMesh && agent.enabled)
        {
            StartCoroutine(RobotHeadCheck());
        }

        // Si la tête du robot est détachée et que l'utilisateur est très proche du robot
        if (/*robotHeadObject.parent == null && */Vector3.Distance(mainCamera.transform.position, robot.transform.position) < 1.0f)
        {
            GrabRobotHead();
            AttachRobotHead();
        }
    }

    private void GrabRobotHead() // Nouvelle fonction pour saisir la tête du robot
    {
        // Code pour saisir la tête du robot
        if (robotHeadObject != null && robotHeadObject.tag == "head") // Si la tête du robot est saisie
        {
            grabbedObject = robotHeadObject; // Définir l'objet saisi comme la tête du robot

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

    IEnumerator RobotHeadCheck()
    {
        while (true)
        {
            if (robotHead.parent == null)
            {
                // Attendez 1 seconde avant d'arrêter le robot
                yield return new WaitForSeconds(1);

                // Si la tête du robot est tombée, changez la destination de l'agent à la position de la tête
                if (agent != null && agent.isOnNavMesh)
                {
                    agent.destination = robotHead.position;
                }
            }
            else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                if (agent != null && agent.isOnNavMesh)
                {
                    agent.destination = patrolPoints[currentPoint].position;
                }
            }

            yield return null;
        }
    }

    IEnumerator LowerArms()
    {
        float elapsedTime = 0;
        // Les rotations initiales pour chaque bras, coude et main
        Quaternion startingRotationLeftArm = Quaternion.Euler(0, -90, 180);
        Quaternion startingRotationRightArm = Quaternion.Euler(0, 90, 0);
        Quaternion startingRotationLeftElbow = Quaternion.Euler(-90, 0, -90);
        Quaternion startingRotationRightElbow = Quaternion.Euler(-90, 0, 90);
        Quaternion startingRotationLeftHand = Quaternion.Euler(-90, 0, 90);
        Quaternion startingRotationRightHand = Quaternion.Euler(-90, 0, 90);

        // Les rotations cibles pour chaque bras, coude et main
        Quaternion targetRotationLeftArm = Quaternion.Euler(60, 0, 110);
        Quaternion targetRotationRightArm = Quaternion.Euler(60, 0, 110);
        Quaternion targetRotationLeftElbow = Quaternion.Euler(0, 0, 90);
        Quaternion targetRotationRightElbow = Quaternion.Euler(0, 0, 90);
        Quaternion targetRotationLeftHand = Quaternion.Euler(0, 90, 0);
        Quaternion targetRotationRightHand = Quaternion.Euler(0, 90, 0);

        while (elapsedTime < armLowerSpeed)
        {
            leftArm.rotation = Quaternion.Slerp(startingRotationLeftArm, targetRotationLeftArm, (elapsedTime / armLowerSpeed));
            rightArm.rotation = Quaternion.Slerp(startingRotationRightArm, targetRotationRightArm, (elapsedTime / armLowerSpeed));
            leftElbow.rotation = Quaternion.Slerp(startingRotationLeftElbow, targetRotationLeftElbow, (elapsedTime / armLowerSpeed));
            rightElbow.rotation = Quaternion.Slerp(startingRotationRightElbow, targetRotationRightElbow, (elapsedTime / armLowerSpeed));
            leftHand.rotation = Quaternion.Slerp(startingRotationLeftHand, targetRotationLeftHand, (elapsedTime / armLowerSpeed));
            rightHand.rotation = Quaternion.Slerp(startingRotationRightHand, targetRotationRightHand, (elapsedTime / armLowerSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
