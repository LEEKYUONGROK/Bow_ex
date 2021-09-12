using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OceanUI : MonoBehaviour
{
    public GameObject canvas;
    
    void Start()
    {
        canvas.SetActive(false);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("canvas come on");
            canvas.SetActive(true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("canvas over");
            canvas.SetActive(false);
        }
    }




}
