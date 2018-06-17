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
		//We render a mesh in there and the remanence will keep frame form the buffer to render remanence*time points in a Mesh
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
				
			//Remanence not implemented yed.
            remanence = 0;
        }

        private void Update()
        {
            if (buffer != null && !buffer.IsEmpty())
            {
				//we dequeue is the buffer is not empty
                Frame vFrame = buffer.Dequeue();

				//we dequeue several if remance activated (>=1)
                if (remanence > 0)
                {
                    if (frames.Count > remanence)
                    {
                        vFrame = frames[0];
                        frames.RemoveAt(0);

                        if (remanence > 0)
                        {
                            foreach (Frame vComposeFrame in frames)
                            {
								//We give all the points of ecaht frame to render a single mesh
                                vFrame.Compose(vComposeFrame);
                            }
                        }
                    }
                    else if (frames.Count <= remanence)
                    {
                        frames.Add(vFrame);
                    }

                }

                try
                {
					//we clear or mesh (can be opti)
                    mesh.Clear();
					//We construct our mesh with vertices and colors
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