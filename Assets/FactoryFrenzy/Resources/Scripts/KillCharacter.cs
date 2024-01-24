using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCharacter : MonoBehaviour
{
    void Update()
    {
        // Vérifie si la touche 'K' est enfoncée
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Détruit le GameObject du personnage
            Destroy(gameObject);
        }
    }
}
