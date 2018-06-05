using TheiaVR.Model;
using System.Collections.Generic;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : KinectRenderer
    {
        private GameObject gameObject;

        private GameObject parent;
        
        private List<GameObject> gameObjects;
        
        private void Awake()
        {
            gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;

            gameObjects = new List<GameObject>();
        }

        public void SetParent(GameObject aParent){
            parent = aParent;
        }
        
        private new void Update()
        {
            
            if (buffer != null && !buffer.IsEmpty())
            {
                
                Frame vFrame = buffer.Dequeue();
                Vector3[] positions = vFrame.GetVectors();
                
                if (gameObject != null && positions != null && gameObjects.Count <= 0 && positions.Length > 0)
                {
                    for (int i = 0; i < positions.Length; i++)
                    {
                        GameObject newCube = Instantiate(gameObject, positions[i], Quaternion.identity);
                        newCube.transform.parent = parent.transform;
                        gameObjects.Add(newCube);
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
        }
    }
}

    