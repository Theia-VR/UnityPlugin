using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Camera mainCamera;

    // For FlyMode
    public float speedKeyBoard = 4f;
    public float speedMouse =2f;

    private float yaw;
    private float pitch;

    public bool flymode;



    // For OrbitMode
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        yaw = 0.0f;
        pitch = 0.0f;
        this._XForm_Camera = this.mainCamera.transform;
        this._XForm_Parent = this.mainCamera.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {

        if (flymode)
        {
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

            yaw += speedMouse * Input.GetAxis("Mouse X");
            pitch -= speedMouse * Input.GetAxis("Mouse Y");

            mainCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
        else
        {

            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 90f)
                    _LocalRotation.y = 90f;
            }
            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 1.5f, 100f);
            }

            // Actual Camera Rig Transformations
            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

            if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
            {
                this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }

        }
    }
}
