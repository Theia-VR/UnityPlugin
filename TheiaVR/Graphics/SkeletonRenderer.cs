using TheiaVR.Model;
using System.Collections.Generic;
using UnityEngine;

namespace TheiaVR.Graphics
{
    class SkeletonRenderer : MonoBehaviour
    {
        private static SkeletonRenderer instance;
        public GameObject obj;
        
        private List<GameObject> gameObjects;
        private List<Vertex> positions;

        public static SkeletonRenderer GetInstance()
        {
            return instance;
        }

        public void Start()
        {
            instance = this;
            gameObjects = new List<GameObject>();
            positions = new List<Vertex>(25);

            for(int i=0; i < 25; i++)
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

        public void UpdatePositions(List<Vertex> aVertexs)
        {
            positions = aVertexs;    
        }
        
    }
}
    