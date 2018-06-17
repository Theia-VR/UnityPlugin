using System;
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
        
        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        private void Update()
        {
            if (frame != null)
            {
                try
                {
					//we clear or mesh (can be opti)
                    mesh.Clear();

                    mesh.vertices = frame.GetVectors();
                    mesh.colors = frame.GetColors();
                    mesh.SetIndices(frame.GetIndices(), MeshTopology.Points, 0);
                    
                    frame = null;
                }
                catch (Exception aException)
                {
                    Messages.LogError(aException.Message);
                }
            }
        }
    }
}