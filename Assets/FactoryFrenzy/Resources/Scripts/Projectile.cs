using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxDistance = 10f; // Ajoutez cette variable pour d�finir la port�e maximale
    public float shootingForce = 1f; // Réduisez cette variable pour une vitesse plus lente

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;

        // Ajoutez la force initiale lorsque le projectile est instanci�
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Désactivez la gravité pour un mouvement horizontal
            rb.AddForce(transform.forward * shootingForce, ForceMode.VelocityChange); // Utilisez VelocityChange pour une vitesse constante
        }
    }

    private void Update()
    {
        // Calculez la distance parcourue par le projectile
        float distance = Vector3.Distance(initialPosition, transform.position);

        // V�rifiez si le projectile a d�pass� sa port�e maximale
        if (distance >= maxDistance)
        {
            // D�truisez le projectile s'il a d�pass� sa port�e maximale
            Destroy(gameObject);
        }
    }
}
