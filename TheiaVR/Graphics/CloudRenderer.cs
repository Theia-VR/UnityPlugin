using System;
using System.Collections.Generic;
using UnityEngine;
using TheiaVR.Helpers;
using TheiaVR.Model;

namespace TheiaVR.Graphics
{
    public class CloudRenderer : MonoBehaviour
    {
        private static CloudRenderer instance;

        private GameObject gameObject;

        private List<GameObject> gameObjects;
        private List<Vertex> positions;

        public static CloudRenderer GetInstance()
        {
            return instance;
        }

        public void Start()
        {
            instance = this;

            gameObjects = new List<GameObject>(3000);
            positions = new List<Vertex>(3000);

            //Instanciation of empty sprite
            Vector2 pivot = new Vector2(1f, 1f);
            Rect rect = new Rect(pivot, new Vector2(1f, 1f));
            Texture2D tex = Texture2D.whiteTexture;
            tex.Apply();
            Sprite sprite = Sprite.Create(tex, rect, pivot);

            //Instanciation of Game Object
            gameObject = new GameObject("Cloud_prefab");

            //Adding a spriteRenderer with a sprite and a color
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.color = new Color(139f / 255f, 69f / 255f, 19f / 255f, 1f);

            //Getting the correct scale
            Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.localScale = scale;
        }

        void Update()
        {

            if (gameObject != null && positions != null && gameObjects.Count < positions.Count)
            {
                int vNbOfGameObjects = gameObjects.Count;
                for (int i = 0; i < positions.Count - gameObjects.Count; i++)
                {
                    if (positions[i] != null)
                    {
                        gameObjects.Add(Instantiate(gameObject, positions[vNbOfGameObjects + i].GetVector(), Quaternion.identity));
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
            if (positions.Count <= 0)
            {
                positions = aVertexs;
            }
            else
            {
                for (int i = 0; i < aVertexs.Count; i++)
                {
                    if (positions[i] != null && !(distanceChange(aVertexs[i].GetVector(), positions[i].GetVector()) < 0.05))
                    {
                        positions[i] = aVertexs[i];
                    }    
                }
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
