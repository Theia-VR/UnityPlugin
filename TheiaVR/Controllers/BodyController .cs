using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers
{
    class BodyController : MonoBehaviour
    {
        private static BodyController instance = null;
        private static int numberOfVertexs = 25;

        private GameObject[] vertexs;

        private BodyController()
        {

            // Used to prevent public access
        }

        public static BodyController GetInstance()
        {
            if (instance == null)
            {
                instance = new BodyController();
                
            }
            return instance;
        }

        public void Start(GameObject vertex_prefabs)
        {
            Messages.Log("Body Controller started");
            vertexs = new GameObject[numberOfVertexs];
            for (int i = 0; i < numberOfVertexs; i++)
            {
                vertexs[i] = Instantiate(vertex_prefabs, new Vector3((float)i, (float)i, (float)i), Quaternion.identity);
            }
            Messages.Log("BodyController : Instantiate all vertexs");
        }

        /*void Update()
        {
            foreach(GameObject go in vertexs)
            {
                go.transform.Translate(Vector3.up * Time.deltaTime);
            }
        }*/

        void FixedUpdate()
        {
            foreach (GameObject go in vertexs)
            {
                go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 10);
            }
        }
    }
}
