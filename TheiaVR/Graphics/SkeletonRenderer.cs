using TheiaVR.Model;
using System.Collections.Generic;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : MonoBehaviour
    {
        public GameObject gameObject;
        
        private List<GameObject> gameObjects;
        
        protected FrameBuffer buffer;

        
        public void SetBuffer(FrameBuffer aBuffer){
            buffer = aBuffer;
        }

        public void Start()
        {
            gameObjects = new List<GameObject>();
//            gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;

//            for(int i=0; i < 25; i++)
//            {
//                gameObjects.Add(Instantiate(gameObject));
//            }
        }

        void Update()
        {
            
            if (buffer != null && !buffer.IsEmpty())
            {
                
                Frame vFrame = buffer.Dequeue();
                Vector3[] positions = vFrame.GetVectors();
                
                if (gameObject != null && positions != null && gameObjects.Count <= 0 && positions.Length > 0)
                {
                    for (int i = 0; i < positions.Length; i++)
                    {
                        gameObjects.Add(Instantiate(gameObject, positions[i], Quaternion.identity));
                    }
                }
                else if (gameObjects != null && positions != null && positions.Length > 0)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        gameObjects[i].transform.SetPositionAndRotation(positions[i], Quaternion.identity);
                    }
                }
            }
            
//            if (gameObjects != null && positions != null && positions.Count > 0)
//            {
//                for (int i = 0; i < gameObjects.Count; i++)
//                {
//                    gameObjects[i].transform.SetPositionAndRotation(positions[i].GetVector(), Quaternion.identity);
//                }
//            }
        }
//
//        public void UpdatePositions(List<Vertex> aVertex)
//        {
//            positions = aVertex;
//        }
    }
}

    