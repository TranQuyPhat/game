using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager2 : MonoBehaviour
{   
    public float vertical, horizontal;
    public bool handbrake;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        vertical= Input.GetAxis("Vertical");
        horizontal= Input.GetAxis("Horizontal");
        handbrake= (Input.GetAxis("Jump")!=0)?true:false;

    }

    // Update is called once per frame
  
}
