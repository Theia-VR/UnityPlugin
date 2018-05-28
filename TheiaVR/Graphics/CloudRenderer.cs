using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheiaVR.Model;
using System.Collections;

namespace TheiaVR.Graphics
{
    class CloudRenderer : MonoBehaviour
    {
        private static CloudRenderer instance;
        public GameObject obj;

        private List<GameObject> gameObjects;
        private List<Vertex> positions;

        public static CloudRenderer GetInstance()
        {
            return instance;
        }

        public void Start()
        {
            instance = this;
            gameObjects = new List<GameObject>();
            positions = new List<Vertex>();

            for (int i = 0; i < 3000; i++)
            {
                gameObjects.Add(Instantiate(obj));
            }
        }

        void Update()
        {

            if (gameObjects != null && positions != null && positions.Count > 0)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].transform.SetPositionAndRotation(positions[i].GetVector(), Quaternion.identity);
                }
            }
        }


        public void UpdatePositionsByFrame(ArrayList frames)
        {
            /*ArrayList newPositions = new List<Vertex>();
            for (int i = 0; i < frames.Count; i++)
            {

            }*/
            int i = 0;
            foreach(Vertex vertex in frames)
            {
                if(distanceChange(positions[i].GetVector(),vertex.GetVector()) < 0.05)
                {
                    //do nothing
                }
                else
                {
                    positions[i] = vertex;
                }
                
                i++;
            }
                        
        }

        private double distanceChange(Vector3 vector31, Vector3 vector32)
        {
            double toto =  Math.Sqrt((vector31.x - vector32.x) * (vector31.x - vector32.x) + (vector31.y - vector32.y) * (vector31.y - vector32.y) + (vector31.z - vector32.z) * (vector31.z - vector32.z));
            Debug.Log(toto);
            return toto;
        }
    }
}
