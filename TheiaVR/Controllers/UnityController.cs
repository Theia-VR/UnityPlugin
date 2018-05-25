using System;
using UnityEngine;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers
{
    class UnityController : MonoBehaviour
    {
        public GameObject vertex;
        private static int numberOfVertexs = 15;

        private GameObject[] vertexs;
        private Vector3[] cubesPositions;

        void OnEnable()
        {
            Messages.Log("Enabling TheiaVR plugin");
        }

        public void Start()
        {
            Messages.Log("Starting TheiaVR plugin");
            try
            {
                StreamController.GetInstance().Start(true, false);
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
            cubesPositions = new Vector3[numberOfVertexs];

            for (int i = 0; i < numberOfVertexs; i++)
            {
                vertexs[i] = Instantiate(vertex, new Vector3((float)i, (float)i, (float)i), Quaternion.identity);
                cubesPositions[i] = new Vector3((float)i, (float)i, (float)i);
            }
            Messages.Log("BodyController : " + vertexs.ToString());
            Messages.Log("BodyController : " + cubesPositions.ToString());
            Messages.Log("BodyController : Instantiate all vertexs");


        }

        void Update()
        {
            //vertexs[0].transform.GetComponent<Rigidbody>().AddForce(transform.forward * 2);
            for(int j=0; j<numberOfVertexs; j++)
            {
                //vector.x = vector.x + UnityEngine.Random.Range(-1.0f, 1.0f);
                cubesPositions[j] = new Vector3(cubesPositions[j].x + UnityEngine.Random.Range(-1.0f, 1.0f), cubesPositions[j].y + UnityEngine.Random.Range(-1.0f, 1.0f), cubesPositions[j].z + UnityEngine.Random.Range(-1.0f, 1.0f));
                cubesPositions[j] = new Vector3(cubesPositions[j].x + UnityEngine.Random.Range(-1.0f, 1.0f), cubesPositions[j].y + UnityEngine.Random.Range(-1.0f, 1.0f), cubesPositions[j].z + UnityEngine.Random.Range(-1.0f, 1.0f));
            }

            for(int i=0; i<numberOfVertexs; i++)
            {
                vertexs[i].transform.SetPositionAndRotation(cubesPositions[i], Quaternion.identity);
            }
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
