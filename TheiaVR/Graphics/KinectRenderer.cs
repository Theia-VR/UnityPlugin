using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Controllers;
using TheiaVR.Helpers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    
    public abstract class KinectRenderer : MonoBehaviour
    {
        private GameObject gameObject;

        protected FrameBuffer buffer;

        protected List<GameObject> gameObjects;

        public void SetBuffer(FrameBuffer aBuffer){
            buffer = aBuffer;
            gameObjects = new List<GameObject>(buffer.GetLength());
        }
        
        protected void SetGameObject(GameObject aGameObject)
        {
            gameObject = aGameObject;
        }
        
        protected void Update()
        {   
            if (buffer != null && gameObjects != null && gameObjects.Count <= 0 && buffer.IsFull())
            {
                List<Vertex> vVertexs = buffer.DequeueAll();
                for (int i = 0; i < vVertexs.Count; i++)
                {
                    if (vVertexs[i] != null)
                    {
                        GameObject vGameObject = Instantiate(gameObject, vVertexs[i].GetVector(), Quaternion.identity);
                        vGameObject.GetComponent<Renderer>().material.color = vVertexs[i].GetColor();

                        gameObjects.Add(vGameObject);
                    }
                }
            }
            else if (gameObjects != null && buffer != null && buffer.IsFull())
            {
                List<Vertex> vVertexs = buffer.DequeueAll();
                for (int i = 0; i < vVertexs.Count; i++)
                {
                    if (vVertexs[i] != null)
                    {
                        gameObjects[i].transform.SetPositionAndRotation(vVertexs[i].GetVector(), Quaternion.identity);
                        gameObjects[i].GetComponent<Renderer>().material.color = vVertexs[i].GetColor();
                    }
                }
            }
        }

        public void DestroyAllObjects()
        {
            foreach (GameObject obj in gameObjects)
            {
                Destroy(obj);
            }
        }

        protected void OnApplicationQuit()
        {
            StreamController.GetInstance().Stop();
        }

    }
}
