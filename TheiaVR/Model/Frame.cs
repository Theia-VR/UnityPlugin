using System.Collections.Generic;
using UnityEngine;

namespace TheiaVR.Model
{
    public class Frame
    {
        private List<Vector3> vectors;

        private List<Color> colors;

        private List<int> indices;

        public Frame()
        {
            vectors = new List<Vector3>();
            colors = new List<Color>();
            indices = new List<int>();
        }
        
        public int GetPointsNumber()
        {
            return vectors.Count;
        }

        public void AddPoint(float aX, float aY, float aZ, byte aR, byte aG, byte aB)
        {
            vectors.Add(new Vector3(aX, aY, aZ));
            colors.Add(new Color(aR / 255f, aG / 255f, aB / 255f));
            indices.Add(indices.Count);
        }
        
        public Vector3[] GetVectors()
        {
            return vectors.ToArray();
        }

        public Color[] GetColors()
        {
            return colors.ToArray();
        }

        public int[] GetIndices()
        {
            return indices.ToArray();
        }

    }
}
