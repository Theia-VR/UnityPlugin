using System.Collections.Generic;
using TheiaVR.Helpers;
using TheiaVR.Model;
using UnityEngine;

namespace TheiaVR.Graphics
{
    [RequireComponent(typeof(MeshFilter))]
    public class CloudRenderer : KinectRenderer
    {
        private static CloudRenderer instance;

        Mesh mesh;

        public static CloudRenderer GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        private void Start()
        {
//            base.Start();
            instance = this;

//            //Instanciation of empty sprite
//            Vector2 vPivot = new Vector2(1f, 1f);
//            Rect vRectangle = new Rect(vPivot, new Vector2(1f, 1f));
//            Texture2D vTexture = Texture2D.whiteTexture;
//            vTexture.Apply();
//            Sprite vSprite = Sprite.Create(vTexture, vRectangle, vPivot);
//
//            //Instanciation of Game Object
//            GameObject vGameObject = new GameObject();
//
//            //Adding a spriteRenderer with a sprite and a color
//            SpriteRenderer renderer = vGameObject.AddComponent<SpriteRenderer>();
//            renderer.sprite = vSprite;
//
//            //Getting the correct scale
//            Vector3 vScale = new Vector3(1.5f, 1.5f, 1.5f);
//            vGameObject.transform.localScale = vScale;
//
//            base.SetGameObject(vGameObject);
        }

        protected void Update()
        {
            if (buffer != null && !buffer.IsEmpty())
            {
                List<Vertex> vVertexs = buffer.DequeueAll();
                Messages.Log("Dequeued mon bro!!");
                Vector3[] points = new Vector3[vVertexs.Count];
                int[] indices = new int[vVertexs.Count];
                Color[] colors = new Color[vVertexs.Count];

                for (int i = 0; i < vVertexs.Count; i++)
                {
                    if (vVertexs[i] != null)
                    {
                        points[i] = new Vector3(vVertexs[i].GetVector().x, vVertexs[i].GetVector().y,
                            vVertexs[i].GetVector().z);
                        indices[i] = i;
                        colors[i] = vVertexs[i].GetColor();
                    }
                }

                mesh.Clear();
                mesh.vertices = points;
                mesh.colors = colors;
                mesh.SetIndices(indices, MeshTopology.Points, 0);
            }
        }
    }
}