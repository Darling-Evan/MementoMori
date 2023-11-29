using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hbRotate : MonoBehaviour
{

    private Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Player/MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.name != "MainCamera") {
            cam = GameObject.Find("Player/MainCamera").GetComponent<Camera>();
        }

        gameObject.transform.rotation = cam.transform.rotation;
    }
}
