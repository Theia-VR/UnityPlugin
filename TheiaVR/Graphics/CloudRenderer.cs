using System;
using System.Collections.Generic;
using TheiaVR.Helpers;
using TheiaVR.Model;
using UnityEngine;

namespace TheiaVR.Graphics
{
    [RequireComponent(typeof(MeshFilter))]
    public class CloudRenderer : KinectRenderer
    {
        private Mesh mesh;

        private int remanence;
        
        private List<Frame> frames;

        public void SetRemanence(int aRemanence)
        {
            remanence = aRemanence;
        }

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            frames = new List<Frame>();
            
            remanence = 0;
        }

        private void Update()
        {
            if (buffer != null && !buffer.IsEmpty())
            {
                Frame vFrame = buffer.Dequeue();
                if (frames.Count >= remanence + 1)
                {
                    vFrame = frames[0];
                    frames.RemoveAt(0);

                    foreach (Frame vComposeFrame in frames)
                    {
                        vFrame.Compose(vComposeFrame);
                    }
                }

                if (frames.Count <= remanence)
                {
                    frames.Add(vFrame);
                }

                try
                {
                    mesh.Clear();
                    mesh.vertices = vFrame.GetVectors();
                    mesh.colors = vFrame.GetColors();
                    mesh.SetIndices(vFrame.GetIndices(), MeshTopology.Points, 0);
                }
                catch (Exception aException)
                {
                    Messages.LogError(aException.Message);
                }
            }
        }
    }
}