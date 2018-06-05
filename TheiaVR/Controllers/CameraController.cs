using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Camera mainCamera;
    float speedKeyBoard;
    float speedMouse;

    float yaw;
    float pitch;



	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        speedKeyBoard = 4f;
        speedMouse = 2f;
        yaw = 0.0f;
        pitch = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        /*float x = mainCamera.transform.position.x;
        float z = mainCamera.transform.position.z;

        mainCamera.transform.SetPositionAndRotation(new Vector3(x, 0f, z), Quaternion.identity);*/

        Vector3 move;

        move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move = new Vector3(speedKeyBoard * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            move = new Vector3(-speedKeyBoard * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move = new Vector3(0, 0, -speedKeyBoard * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            move = new Vector3(0, 0, speedKeyBoard * Time.deltaTime);
        }

        mainCamera.transform.Translate(move);

        /*float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        //rotation = new Vector3(-mouseInputY, mouseInputX, 0);
        rotation = new Vector3(0, mouseInputX, 0);

       
        mainCamera.transform.eulerAngles = rotation;*/

        yaw += speedMouse * Input.GetAxis("Mouse X");
        pitch -= speedMouse * Input.GetAxis("Mouse Y");

        mainCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);

    }
}
