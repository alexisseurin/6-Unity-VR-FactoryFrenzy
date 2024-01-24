using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPlateforme : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float vitesseDeplacement = 2.0f;

    private Vector3 destination;
    private Transform playerOnPlatform;
    public Vector3 lastPlatformPosition;

    void Start()
    {
        destination = pointB;
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        DeplacerPlateforme();
    }

    void DeplacerPlateforme()
    {
        // Déplacer la plateforme vers la destination
        transform.position = Vector3.MoveTowards(transform.position, destination, vitesseDeplacement * Time.deltaTime);

        // Vérifier si un personnage est sur la plateforme
        if (playerOnPlatform != null)
        {
            // Calculer le déplacement de la plateforme
            Vector3 deplacementPlateforme = transform.position - lastPlatformPosition;

            // Appliquer le déplacement de la plateforme au personnage
            playerOnPlatform.position += deplacementPlateforme;
        }

        // Vérifier si la plateforme a atteint la destination
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            // Inverser la direction pour alterner entre les points A et B
            destination = (destination == pointA) ? pointB : pointA;
        }

        // Mettre à jour la dernière position de la plateforme
        lastPlatformPosition = transform.position;
    }
}
