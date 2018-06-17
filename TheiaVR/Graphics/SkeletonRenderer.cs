using TheiaVR.Model;
using System.Collections.Generic;
using UnityEngine;
using TheiaVR.Helpers;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : KinectRenderer
    {
        private new GameObject gameObject;
        
        private List<GameObject> gameObjects;
        
        private void Awake()
        {
			//We need to load ressource for render a prefabs for a part of our skeleton
            gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;

            gameObjects = new List<GameObject>();
        }
        
        private void Update()
        {
            
            if (frame != null)
            {
                Vector3[] vPositions = frame.GetVectors();
                
				//if we don't have any game objects instanciated, we instanciate them
                if (gameObject != null && vPositions != null && gameObjects.Count <= 0 && vPositions.Length > 0)
                {
                    for (int i = 0; i < vPositions.Length; i++)
                    {
                        GameObject vGameObject = Instantiate(gameObject, vPositions[i], Quaternion.identity);
                        vGameObject.transform.parent = this.transform;
                        gameObjects.Add(vGameObject);
                    }
                }
			    //if we have all ouyr gameObjects, we set their absolute position
                else if (gameObjects != null && vPositions != null && vPositions.Length > 0)
                {
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
						
                        gameObjects[i].transform.SetPositionAndRotation(vPositions[i], Quaternion.identity);
                    }
                }

                frame = null;
            }
        }
    }
}

    