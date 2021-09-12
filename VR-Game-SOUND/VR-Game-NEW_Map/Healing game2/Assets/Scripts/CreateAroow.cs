using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAroow : MonoBehaviour
{
    public GameObject MY_Arrow;
   
    public void Create_arrow()
    {
        Instantiate(MY_Arrow, this.transform);
    }
}
