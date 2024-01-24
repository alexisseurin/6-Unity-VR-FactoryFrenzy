using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    public GameObject axle; // Ajoutez cette ligne pour sélectionner l'axle dans l'inspecteur
    public GameObject projectilePrefab;
    public float shootingForce = 0.5f; // Réduisez cette valeur pour une vitesse de tir plus lente
    public float shootingInterval = 2f;
    public float rotationSpeed = 10f; // Vitesse de rotation en degrés par seconde

    private void Start()
    {
        // Instancier le projectile au début
        Shoot();

        // Utilisez une coroutine pour tirer périodiquement
        StartCoroutine(ShootProjectiles());
    }

    private void Update()
    {
        // Fait tourner le shooter autour de son axe Y à chaque frame
        axle.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private IEnumerator ShootProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);
            Shoot();
        }
    }

    private void Shoot()
    {
        // la distance devant le shooter à laquelle le projectile est instancié
        //float spawnDistance = 1.5f;
        Vector3 spawnPosition = axle.transform.position + axle.transform.forward; //* spawnDistance;

        // 0.5 à la position y
        spawnPosition.y += 0.5f;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, axle.transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            // Désactiver la gravité pour le projectile
            projectileRb.useGravity = false;

            // Appliquer une force vers l'avant (dans la direction du transform.forward)
            Vector3 shootingDirection = axle.transform.forward; // Essayez également avec transform.up, transform.right, etc.
            projectileRb.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);

            // Debug.Log("Projectile direction: " + shootingDirection);
            // Debug.Log("Projectile force: " + shootingDirection * shootingForce);

            // Commencez une coroutine pour détruire le projectile après 2 secondes
            StartCoroutine(DestroyProjectileAfterDelay(projectile, 1.2f));
        }
    }

    private IEnumerator DestroyProjectileAfterDelay(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }
}
