using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheiaVR.Graphics;
using TheiaVR.Helpers;

public class InstantiateObjects : MonoBehaviour {

    private Camera mainCamera;
    private GameObject repere;

    // Use this for initialization
    void Start () {
        /*mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<TheiaVR.Graphics.SkeletonRenderer>();*/


        GameObject[] reperes = GameObject.FindGameObjectsWithTag("MainCamera");
        repere = reperes[0];
        Messages.Log(repere.tag);

        repere.AddComponent<TheiaVR.Graphics.SkeletonRenderer>();
        Messages.Log(repere.tag);

        //GameObject.FindGameObjectWithTag("Repere").GetComponent<>;*/
    }

    // Update is called once per frame
    void Update () {
		
	}
}
