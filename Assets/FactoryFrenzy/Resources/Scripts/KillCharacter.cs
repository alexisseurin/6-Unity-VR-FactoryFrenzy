using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCharacter : MonoBehaviour
{
    void Update()
    {
        // V�rifie si la touche 'K' est enfonc�e
        if (Input.GetKeyDown(KeyCode.K))
        {
            // D�truit le GameObject du personnage
            Destroy(gameObject);
        }
    }
}
