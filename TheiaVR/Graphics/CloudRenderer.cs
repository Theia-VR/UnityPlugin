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

            Vector2 pivot = new Vector2(1f, 1f);
            Rect rect = new Rect(pivot, new Vector2(1f, 1f));
            //Texture2D texture = Texture2D.whiteTexture;

            // Create a 16x16 texture with PVRTC RGBA4 format
            // and will it with raw PVRTC bytes.
            Texture2D tex = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);
            // Raw PVRTC4 data for a 16x16 texture. This format is four bits
            // per pixel, so data should be 16*16/2=128 bytes in size.
            // Texture that is encoded here is mostly green with some angular
            // blue and red lines.
            byte[] pvrtcBytes = new byte[] {
            0x00, 0x00, 0x00, 0x32, 0xe7, 0x30, 0xaa, 0x7f, 0x32, 0x32, 0x32, 0x32, 0xf9, 0x40, 0xbc, 0x7f,
            0x00, 0x00, 0x00, 0x03, 0xf6, 0x30, 0x02, 0x05, 0x03, 0x03, 0x03, 0x03, 0xf4, 0x30, 0x03, 0x06,
            0x00, 0x00, 0x00, 0x32, 0xf7, 0x40, 0xaa, 0x7f, 0x32, 0xf2, 0x02, 0xa8, 0xe7, 0x30, 0xff, 0xff,
            0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x00, 0x0f, 0x00, 0xff, 0x00, 0xaa, 0xe9, 0x40, 0x9f, 0xff,
            0x00, 0x00, 0x00, 0x03, 0xca, 0x6a, 0x0f, 0x30, 0x03, 0x03, 0x03, 0xff, 0xca, 0x68, 0x0f, 0x30,
            0x00, 0x00, 0x90, 0x40, 0xba, 0x5b, 0xaf, 0x68, 0x40, 0x00, 0x00, 0xff, 0xca, 0x58, 0x0f, 0x20,
            0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x01, 0x2c, 0x00, 0xff, 0x00, 0xaa, 0xdb, 0x41, 0xff, 0xff,
            0x00, 0x00, 0x00, 0xff, 0xe8, 0x40, 0x01, 0x1c, 0x00, 0xff, 0x00, 0xaa, 0xbb, 0x40, 0xff, 0xff,
        };
            // Load data into the texture and upload it to the GPU.
            tex.LoadRawTextureData(pvrtcBytes);
            tex.Apply();

            Sprite sprite = Sprite.Create(tex, rect, pivot);

            gameObject = new GameObject("Cloud_prefab");

            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;

            Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
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
