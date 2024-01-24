using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBackBumper : MonoBehaviour
{
    public float bumperForce = 25f; // Ajustez la force au besoin

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Appeler la fonction Push dans le script ThirdPersonController
            ThirdPersonController playerController = collision.gameObject.GetComponent<ThirdPersonController>();
            if (playerController != null)
            {
                // Utiliser la fonction Push du ThirdPersonController
               // playerController.Push(transform.up, bumperForce);
            }
        }
    }
}
