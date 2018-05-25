using System;
using UnityEngine;
using TheiaVR.Helpers;
using TheiaVR.Model;

namespace TheiaVR.Controllers
{
    class UnityController : MonoBehaviour
    {
        
        public GameObject gameObject;
        
        private GameObject[] gameObjects;

        private static Vertex[] vertexs;

        void OnEnable()
        {
            Messages.Log("Enabling TheiaVR plugin");
            
        }

        public void Start()
        {
            Messages.Log("Starting TheiaVR plugin");
            try
            {
                if (gameObjects == null || gameObjects.Length <= 0)
                {
                    gameObjects = new GameObject[25];
                    for (int i = 0; i < 25; i++)
                    {
                        gameObjects[i] = Instantiate(gameObject, new Vector3((float)i, (float)i, (float)i), Quaternion.identity);
                    }
                }
                
            }
            catch(Exception vException)
            {
                Messages.Log(vException.ToString());
            }
            
            Messages.Log("TheiaVR correctly started");
        }
        
        void Update()
        {

            if(vertexs != null && vertexs.Length > 0){
                
                for(int i = 0; i < gameObjects.Length ; i++)
                {
                    System.Random rnd = new System.Random();
                    gameObjects[i].transform.SetPositionAndRotation(new Vector3((float)rnd.Next(1, 11), (float)rnd.Next(1, 11), (float)rnd.Next(1, 11)), Quaternion.identity);   
                }
            }
        }

        public static void InitObjects(Vertex[] aVertexs){
            lock (vertexs)
            {
                vertexs = aVertexs;
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
