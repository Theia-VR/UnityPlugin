using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Camera mainCamera;
    float speed;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        speed = 2f;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 rotation;
        Vector3 move;

        move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move = new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move = new Vector3(0, 0, speed * Time.deltaTime);
        }

        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        //rotation = new Vector3(-mouseInputY, mouseInputX, 0);
        rotation = new Vector3(0, mouseInputX, 0);

        mainCamera.transform.Translate(move);
        mainCamera.transform.Rotate(rotation);

    }
}
