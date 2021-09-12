using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject goPrefab;
    GameObject go;
    public float speed = 10f;
    public void Spawn()
    {
         go = Instantiate(goPrefab,
            transform.position + transform.forward * 0.5f,
            Quaternion.identity);
    }
    public void Shoot()
    {
        if(go != null)
            go.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
}
