using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Ajout du namespace pour XR Rig
using UnityEngine.AI; // Ajout du namespace pour NavMesh

public class RadialMenu : MonoBehaviour
{
    public Transform selectionTransform, cursorTransform;

    public RadialSection top, right, bottom, left;

    public Vector2 touchPosition = Vector2.zero;
    public GameObject robot, gameCamera, teleport; // Ajout d'un GameObject pour le robot, pour la caméra de jeu et pour le point de téléportation

    private readonly float degreeIncrement = 90.0f;  // Ajout d'une variable pour l'incrémentation des degrés

    private XRController xrController; // Ajout d'une variable pour le XRController

    //private NavMeshAgent robotAgent; // Ajout d'une variable pour le NavMeshAgent du robot

    public GameObject robotRay; // Ajout d'un GameObject pour le robot ray



    private void Awake()
    {
        //CreateAndSetupSection();
        xrController = GetComponent<XRController>(); // Récupération du XRController actuel
        //robotAgent = robot.GetComponent<NavMeshAgent>(); // Récupération du NavMeshAgent du robot
    }

    // Start is called before the first frame update
    void Start()
    {
        Show(false); // Le menu est caché au départ
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero + touchPosition;
        float rotation = GetDegree(direction);
        SetCursorPosition();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }

    private void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0,0,-snappedRotation);
    }

    private float SnapRotation(float rotation)
    {
        return GetNearestIncrement(rotation) * degreeIncrement;
    }

    private int GetNearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation / degreeIncrement);
    }

    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetNearestIncrement(currentRotation);

        // Si la section supérieure est sélectionnée, déplace le robot vers la caméra
        if (index == 0) // Supposons que l'index 0 représente la section supérieure
        {
            StartCoroutine(MoveRobotToCamera()); // Démarre la coroutine pour déplacer le robot vers la caméra
        }

        // Si la section "go to" est sélectionnée, déplace le robot vers le point de téléportation
        if (index == 3) // Supposons que l'index 1 représente la section "go to"
        {
            StartCoroutine(MoveRobotToTeleport()); // Démarre la coroutine pour déplacer le robot vers le point de téléportation
        }

        // Si la section inférieure est sélectionnée, ferme le menu
        if (index == 2) // Supposons que l'index 2 représente la section inférieure
        {
            touchPosition = Vector2.zero; // Réinitialise la position du toucher
            Show(false); // Ferme le menu
        }

        
    }

    // Coroutine pour déplacer le robot vers la caméra
    IEnumerator MoveRobotToCamera()
    {
        float duration = 1.0f; // Durée du mouvement en secondes
        float elapsedTime = 0.0f; // Temps écoulé depuis le début du mouvement

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Incrémente le temps écoulé
            float t = elapsedTime / duration; // Calcule la fraction du mouvement effectué

            // Déplace le robot vers la caméra en utilisant le NavMeshAgent
            //robotAgent.SetDestination(Vector3.Lerp(robot.transform.position, gameCamera.transform.position, t));

            yield return null; // Attend la prochaine frame
        }

        //robotAgent.SetDestination(gameCamera.transform.position); // Assure que le robot atteint la position de la caméra
    }

    // Coroutine pour déplacer le robot vers le point où pointe le robot ray
    IEnumerator MoveRobotToTeleport()
    {
        float duration = 1.0f; // Durée du mouvement en secondes
        float elapsedTime = 0.0f; // Temps écoulé depuis le début du mouvement

        // Utilise un raycast pour déterminer le point où pointe le robot ray
        RaycastHit hit;
        if (Physics.Raycast(robotRay.transform.position, robotRay.transform.forward, out hit))
        {
            Vector3 targetPosition = hit.point; // Le point où pointe le robot ray

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime; // Incrémente le temps écoulé
                float t = elapsedTime / duration; // Calcule la fraction du mouvement effectué

                // Déplace le robot vers le point où pointe le robot ray en utilisant le NavMeshAgent
                //robotAgent.SetDestination(Vector3.Lerp(robot.transform.position, targetPosition, t));

                yield return null; // Attend la prochaine frame
            }

            //robotAgent.SetDestination(targetPosition); // Assure que le robot atteint le point où pointe le robot ray
        }
    }


    public void ActivateHighlightedSection()
    {
        Show(true);
    }

    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }

    private float GetDegree(Vector2 direction)
    {
        float value = Mathf.Atan2(direction.x, direction.y);
        value *= Mathf.Rad2Deg;

        if (value < 0)
        {
            value += 360f;
        }

        return value;
    }

    private void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition;
    }

    public void SetTouchPosition(Vector2 newValue)
    {
        touchPosition = newValue;
    }
}
