using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Helpers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : MonoBehaviour
    {
        private static SkeletonRenderer instance;
        public GameObject gameObject;

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
            gameObject = Resources.Load("Vertex_prefabs", typeof(GameObject)) as GameObject;
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
    