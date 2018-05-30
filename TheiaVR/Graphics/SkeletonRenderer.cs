using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Helpers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : MonoBehaviour
    {
        private static SkeletonRenderer instance;
        private GameObject gameObject;

        private List<GameObject> gameObjects;
        private List<Vertex> positions;
        
        public static SkeletonRenderer GetInstance()
        {
            return instance;
        }

        public void Start()
        {
            instance = this;

            gameObjects = new List<GameObject>(25);
            positions = new List<Vertex>(25);
            //gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;
            /*gameObject = new GameObject("Vertex_prefab", 
                new Transform(),

                
                )*/
            gameObject =  GameObject.CreatePrimitive(PrimitiveType.Cube);

            Color color = new Color(139f / 255f, 69f / 255f, 19f / 255f, 1f);
            Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.GetComponent<Renderer>().material.color = color;
            gameObject.transform.localScale = scale;
        }

        void Update()
        {

            if (gameObject != null && positions != null && gameObjects.Count <= 0 && positions.Count > 0)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i] != null)
                    {
                        gameObjects.Add(Instantiate(gameObject, positions[i].GetVector(), Quaternion.identity));
                    }
                }
            }
            else if (gameObjects != null && positions != null && positions.Count > 0)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if(positions[i] != null)
                    {
                        gameObjects[i].transform.SetPositionAndRotation(positions[i].GetVector(), Quaternion.identity);
                    }
                }
            }
        }

        public void UpdatePositions(List<Vertex> aVertexs)
        {
            positions = aVertexs;
        }
    }
}
    