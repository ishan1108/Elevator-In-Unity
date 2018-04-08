using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public Collider coll;
    public GameObject up;
    public GameObject down;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void OnTriggerStay () {
        if (Input.GetKey(KeyCode.U))
        { 
             up.GetComponent<Renderer>().material.color = Color.blue;
        }

        if (Input.GetKey(KeyCode.H))
        {
            down.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}


