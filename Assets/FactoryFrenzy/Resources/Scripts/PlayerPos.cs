using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private SavePoint sp;
    void Start()
    {
        sp = GameObject.FindGameObjectWithTag("SP").GetComponent<SavePoint>();
        transform.position = sp.lastCheckPointPos;
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/
    }
}
