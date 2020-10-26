using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	//public float speed;
	float horizontalSpeed = 2.0f;
    //float verticalSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
    	// La caméra externe tourne autour du personnage sans qu'il bouge
        //transform.Rotate(0,speed*Time.deltaTime,0);
        // Get the mouse delta. This is not in the range -1...1
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Pressed secondary button.");
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);
        }
        /*
        if (Input.GetMouseButton(0))
        {
        	//transform.rotation = Quaternion.Euler(0,0,0);
        	transform.rotation = Quaternion.identity;
        }
        */

        //float v = verticalSpeed * Input.GetAxis("Mouse Y");
        //transform.Rotate(v, h, 0);

        
    }
}
