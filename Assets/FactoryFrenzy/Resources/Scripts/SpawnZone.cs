using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [Header("Spawn Zone Settings")]
    public float width = 10f;
    public float length = 10f;
    public Vector3 spawnOffset; // Nouvelle variable pour le décalage

    private void OnDrawGizmosSelected()
    {
        // Dessine le rectangle de la zone de spawn dans l'éditeur Unity avec le décalage
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + spawnOffset, new Vector3(width, 0f, length));
    }
}
