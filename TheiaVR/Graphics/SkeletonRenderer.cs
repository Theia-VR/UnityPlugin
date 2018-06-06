using TheiaVR.Model;
using System.Collections.Generic;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : KinectRenderer
    {
        private new GameObject gameObject;
        
        private List<GameObject> gameObjects;
        
        private void Awake()
        {
            gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;

            gameObjects = new List<GameObject>();
        }
        
        private void Update()
        {
            
            if (buffer != null && !buffer.IsEmpty())
            {
                
                Frame vFrame = buffer.Dequeue();
                Vector3[] vPositions = vFrame.GetVectors();
                
                if (gameObject != null && vPositions != null && gameObjects.Count <= 0 && vPositions.Length > 0)
                {
                    for (int i = 0; i < vPositions.Length; i++)
                    {
                        GameObject vGameObject = Instantiate(gameObject, vPositions[i], Quaternion.identity);
                        vGameObject.transform.parent = this.transform;
                        gameObjects.Add(vGameObject);
                    }
                }
                else if (gameObjects != null && vPositions != null && vPositions.Length > 0)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        gameObjects[i].transform.SetPositionAndRotation(vPositions[i], Quaternion.identity);
                    }
                }
            }
        }
    }
}

    