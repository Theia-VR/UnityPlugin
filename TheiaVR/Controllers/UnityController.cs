using System;
using UnityEngine;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers
{
    public class UnityController : MonoBehaviour
    {
        public GameObject vertex;

        private static int numberOfVertexs = 15;
        private GameObject[] vertexs;

        void OnEnable()
        {
            Messages.Log("Enabling TheiaVR plugin");
        }

        void Start()
        {
            Messages.Log("Starting TheiaVR plugin");
            try
            {
                StreamController.GetInstance().Start("127.0.0.1", 11000);
                startBodyController();   
            }
            catch(Exception vException)
            {
                Messages.Log("<color=red>" + vException.Message + "</color>");
            }
            
            Messages.Log("TheiaVR correctly started");
        }

        private void startBodyController()
        {
            Messages.Log("Body Controller started");
            vertexs = new GameObject[numberOfVertexs];
            for (int i = 0; i < numberOfVertexs; i++)
            {
                vertexs[i] = Instantiate(vertex, new Vector3((float)i, (float)i, (float)i), Quaternion.identity);
            }
            Messages.Log("BodyController : " + vertexs.ToString());
            Messages.Log("BodyController : Instantiate all vertexs");
        }

        void Update()
        {
            vertexs[0].transform.GetComponent<Rigidbody>().AddForce(transform.forward * 2);
        }

        void OnApplicationQuit()
        {
            Messages.Log("Stopping TheiaVR plugin");
            try
            {
                StreamController.GetInstance().Stop();
            }
            catch (Exception vException)
            {
                Messages.Log("<color=red>" + vException.Message + "</color>");
            }
        }

        void OnDisable()
        {
            Messages.Log("Disabling TheiaVR plugin");
        }
    }

}
