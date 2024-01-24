using UnityEngine;
using UnityEngine.Events;

public class InvisibleWall : MonoBehaviour
{
    public float delayInSeconds = 5f;
    private Collider wallCollider;

    public UnityEvent onWallActivation; // Événement signalant quand le mur devient actif

    private void Start()
    {
        // Désactivez le Collider du mur invisible au début
        wallCollider = GetComponent<Collider>();
        wallCollider.enabled = false;
    }

    private void Update()
    {
        // Supprimez cette partie si vous ne souhaitez pas démarrer le décompte dans Update
        if (wallCollider.enabled)
        {
            delayInSeconds -= Time.deltaTime;

            if (delayInSeconds <= 0f)
            {
                wallCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifiez si le joueur entre en collision avec le mur invisible
        if (other.CompareTag("Player"))
        {
            // Activez le Collider du mur invisible
            wallCollider.enabled = true;

            // Lancez l'événement d'activation du mur
            onWallActivation.Invoke();
        }
    }
}
