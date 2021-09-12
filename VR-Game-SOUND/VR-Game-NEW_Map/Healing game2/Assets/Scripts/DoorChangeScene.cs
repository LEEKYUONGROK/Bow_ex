using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DoorChangeScene : MonoBehaviour
{

    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            SceneManager.LoadScene("Map");

        }

        if (other.gameObject.tag == "player")
        {

            SceneManager.LoadScene("Room");

        }
    }
}

   
