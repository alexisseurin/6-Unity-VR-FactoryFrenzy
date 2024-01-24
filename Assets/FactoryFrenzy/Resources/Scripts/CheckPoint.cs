using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

  

        private SavePoint sp;
    void Start()
    {
            sp = GameObject.FindGameObjectWithTag("SP").GetComponent<SavePoint>();
        }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            sp.lastCheckPointPos = transform.position;
            Debug.Log("Checkpoint reached" + sp.lastCheckPointPos);
        }
    }   
}
